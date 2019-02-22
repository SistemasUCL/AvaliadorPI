using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootProfessor.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProfessor
{
    public class ProfessorService : IProfessorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IUnitOfWork UoW, IProfessorRepository professorRepository)
        {
            _uow = UoW;
            _professorRepository = professorRepository;
        }

        public async Task<Professor> ObterPorId(Guid id)
        {
            return await _professorRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {

            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _professorRepository.CountAsync();
                result.Data = (await _professorRepository.GetAllAsync())
                    .Select(x => new { x.Id, x.Matricula, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _professorRepository.GetAllAsync() :
                    await _professorRepository.SearchAsync(x =>
                        x.Matricula.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
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
                     request.OrderBy == "matricula" ? data.OrderByDescending(x => x.Matricula) :
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderByDescending(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderByDescending(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderByDescending(x => x.Usuario.SobreNome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "matricula" ? data.OrderBy(x => x.Matricula) :
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Usuario.Nome) :
                     request.OrderBy == "email" ? data.OrderBy(x => x.Usuario.Email) :
                     request.OrderBy == "telefone" ? data.OrderBy(x => x.Usuario.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderBy(x => x.Usuario.SobreNome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data.Select(x => new { x.Id, x.Matricula, x.Usuario.Nome, x.Usuario.SobreNome, x.Usuario.Telefone, x.Usuario.Email });

            return result;
        }

        public async Task<SubmitResult<Professor>> Cadastrar(Professor entity)
        {
            var result = await new CadastrarProfessorValidator(_professorRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _professorRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Professor>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Professor>> Remover(Guid id)
        {
            var entity = _professorRepository.GetById(id);

            var result = new ExcluirProfessorValidator(_professorRepository).Validate(entity);

            if (result.IsValid)
            {
                _professorRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Professor>(entity, result);
        }

        public async Task<SubmitResult<Professor>> Editar(Guid id, Professor entity)
        {
            var atual = await ObterPorId(id);

            atual.Matricula = entity.Matricula;

            var result = new EditarProfessorValidator(_professorRepository).Validate(atual);

            if (result.IsValid)
            {
                _professorRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Professor>(atual, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _professorRepository.AnyAsync(x => x.Id == id);
        }
    }
}
