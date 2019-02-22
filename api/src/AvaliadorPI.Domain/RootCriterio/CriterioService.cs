using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootCriterio.Validators;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootCriterio
{
    public class CriterioService : ICriterioService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICriterioRepository _criterioRepository;

        public CriterioService(IUnitOfWork UoW, ICriterioRepository criterioRepository)
        {
            _uow = UoW;
            _criterioRepository = criterioRepository;
        }

        public async Task<Criterio> ObterPorId(Guid id)
        {
            return await _criterioRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterTodosPorProjeto(Guid projetoId, GridRequest request = null)
        {
            var result = new GridResult();

            var data = (await _criterioRepository.SearchAsync(x => x.ProjetoId == projetoId))
                    .OrderBy(x => x.Ordem).Select(x => x);


            if (request == null)
            {
                result.Total = data.Count();
                result.Data = data.Select(x => new { x.Id, x.ProjetoId, x.Ordem, x.Titulo, x.Descricao, x.Peso });
                return result;
            }

            data = string.IsNullOrWhiteSpace(request.Search) ?
                    data : data.Where(x =>
                        x.Titulo.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Descricao.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "titulo" ? data.OrderByDescending(x => x.Titulo) :
                     request.OrderBy == "peso" ? data.OrderByDescending(x => x.Peso) :
                     request.OrderBy == "ordem" ? data.OrderByDescending(x => x.Ordem) :
                     request.OrderBy == "descricao" ? data.OrderByDescending(x => x.Descricao) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "titulo" ? data.OrderBy(x => x.Titulo) :
                     request.OrderBy == "peso" ? data.OrderBy(x => x.Peso) :
                     request.OrderBy == "ordem" ? data.OrderBy(x => x.Ordem) :
                     request.OrderBy == "descricao" ? data.OrderBy(x => x.Descricao) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.ProjetoId, x.Ordem, x.Titulo, x.Descricao, x.Peso });

            return result;
        }

        public async Task<SubmitResult<Criterio>> Cadastrar(Criterio entity)
        {
            var result = await new CadastrarCriterioValidator().ValidateAsync(entity);

            if (result.IsValid)
            {
                _criterioRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Criterio>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Criterio>> Editar(Guid id, Criterio entity)
        {
            var atual = await ObterPorId(id);

            atual.Descricao = entity.Descricao;
            atual.Ordem = entity.Ordem;
            atual.Peso = entity.Peso;
            atual.ProjetoId = entity.ProjetoId;
            atual.Titulo = entity.Titulo;

            var result = await new EditarCriterioValidator().ValidateAsync(atual);

            if (result.IsValid)
            {
                _criterioRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Criterio>(atual, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _criterioRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<SubmitResult<Criterio>> Remove(Guid id)
        {
            var result = new ValidationResult();

            if (result.IsValid)
            {
                _criterioRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Criterio>(null, result);
        }
    }
}