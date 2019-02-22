using AvaliadorPI.Domain.Extensions;
using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootUsuario.Validators;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootUsuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUnitOfWork UoW, IUsuarioRepository usuarioRepository)
        {
            _uow = UoW;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<GridResult> ObterDadosGrid(GridRequest request = null)
        {
            var result = new GridResult();

            if (request == null)
            {
                result.Total = await _usuarioRepository.CountAsync();
                result.Data = await _usuarioRepository.GetAllAsync();
                return result;
            }

            var data = string.IsNullOrWhiteSpace(request.Search) ?
                    await _usuarioRepository.GetAllAsync() :
                    await _usuarioRepository.SearchAsync(x =>
                        x.Nome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.SobreNome.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Telefone.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        x.Email.IndexOf(request.Search, StringComparison.OrdinalIgnoreCase) >= 0);

            result.Total = data.Count();

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                if (request.Direction == Direction.Descendent)
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderByDescending(x => x.Nome) :
                     request.OrderBy == "email" ? data.OrderByDescending(x => x.Email) :
                     request.OrderBy == "telefone" ? data.OrderByDescending(x => x.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderByDescending(x => x.SobreNome) : data;
                }
                else
                {
                    data =
                     request.OrderBy == "nome" ? data.OrderBy(x => x.Nome) :
                     request.OrderBy == "email" ? data.OrderBy(x => x.Email) :
                     request.OrderBy == "telefone" ? data.OrderBy(x => x.Telefone) :
                     request.OrderBy == "sobreNome" ? data.OrderBy(x => x.SobreNome) : data;
                }
            }

            if (request.PageSize > 0 && request.Page > 0)
                data = data.Skip((request.Page - 1) * request.PageSize).Take(request.Page * request.PageSize);

            result.Data = data;

            return result;
        }

        public async Task<SubmitResult<Usuario>> Cadastrar(Usuario entity)
        {
            var result = await new CadastrarUsuarioValidator(_usuarioRepository).ValidateAsync(entity);

            if (result.IsValid)
            {
                _usuarioRepository.Add(entity);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Usuario>(await ObterPorId(entity.Id), result);
        }

        public async Task<SubmitResult<Usuario>> Editar(Guid id, Usuario entity)
        {
            var atual = await ObterPorId(id);

            atual.Email = entity.Email;
            atual.Nome = entity.Nome;
            atual.SobreNome = entity.SobreNome;
            atual.Telefone = entity.Telefone;

            var result = await new EditarUsuarioValidator(_usuarioRepository).ValidateAsync(atual);

            if (result.IsValid)
            {
                _usuarioRepository.Update(atual);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Usuario>(atual, result);
        }

        public async Task<SubmitResult<Usuario>> Remover(Guid id)
        {
            var result = new ValidationResult();

            if (result.IsValid)
            {
                _usuarioRepository.Remove(id);
                await _uow.CommitAsync();
            }

            return new SubmitResult<Usuario>(null, result);
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _usuarioRepository.AnyAsync(x => x.Id == id);
        }
    }
}