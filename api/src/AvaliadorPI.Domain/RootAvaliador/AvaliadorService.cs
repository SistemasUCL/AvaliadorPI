using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootAvaliador.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliador
{
    public class AvaliadorService : IAvaliadorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAvaliadorRepository _avaliadorRepository;

        public AvaliadorService(IUnitOfWork UoW, IAvaliadorRepository avaliadorRepository)
        {
            _uow = UoW;
            _avaliadorRepository = avaliadorRepository;
        }

        public async Task<Avaliador> ObterPorId(Guid id)
        {
            return await _avaliadorRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {
            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _avaliadorRepository.CountAsync();
                result.Data = (await _avaliadorRepository.GetAllAsync())
                    .Select(x => new { x.Id, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _avaliadorRepository.GetAllAsync() :
                    await _avaliadorRepository.SearchAsync(x =>
                        x.Usuario.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.SobreNome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Email.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Usuario.Telefone.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderByDescending(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderByDescending(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderByDescending(x => x.Usuario.SobreNome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderBy(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderBy(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderBy(x => x.Usuario.SobreNome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });

            return result;
        }

        public async Task<SubmitResult<Avaliador>> Cadastrar(Avaliador entity)
        {
            var result = await new CadastrarAvaliadorValidator(_avaliadorRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _avaliadorRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Avaliador>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Avaliador>> Remover(Guid id)
        {
            var result = new ValidationResult();

            if (result.IsValid)
            {
                _avaliadorRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Avaliador>(null, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _avaliadorRepository.AnyAsync(x => x.Id == id);
        }
    }
}
