using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootDisciplina.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootDisciplina
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(IUnitOfWork UoW, IDisciplinaRepository disciplinaRepository)
        {
            _uow = UoW;
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task<Disciplina> ObterPorId(Guid id)
        {
            return await _disciplinaRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {
            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _disciplinaRepository.CountAsync();
                result.Data = (await _disciplinaRepository.GetAllAsync())
                    .Select(x => new { x.Id, x.Nome });
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _disciplinaRepository.GetAllAsync() :
                    await _disciplinaRepository.SearchAsync(x => x.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Nome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Nome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Nome });

            return result;
        }

        public async Task<SubmitResult<Disciplina>> Cadastrar(Disciplina entity)
        {
            var result = await new CadastrarDisciplinaValidator(_disciplinaRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _disciplinaRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Disciplina>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Disciplina>> Editar(Guid id, Disciplina entity)
        {
            var atual = await ObterPorId(id);

            atual.Nome = entity.Nome;

            var result = await new EditarDisciplinaValidator(_disciplinaRepository).ValidateAsync(atual);

            if (result.IsValid)
            {
                _disciplinaRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Disciplina>(atual, result);
        }

        public async Task<SubmitResult<Disciplina>> Remover(Guid id)
        {
            var entity = _disciplinaRepository.GetById(id);

            var result = new ExcluirDisciplinaValidator(_disciplinaRepository).Validate(entity);

            if (result.IsValid)
            {
                _disciplinaRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Disciplina>(entity, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _disciplinaRepository.AnyAsync(x => x.Id == id);
        }
    }
}
