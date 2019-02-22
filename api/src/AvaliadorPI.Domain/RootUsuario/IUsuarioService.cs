using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootUsuario
{
    public interface IUsuarioService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Usuario> ObterPorId(Guid id);
        Task<SubmitResult<Usuario>> Cadastrar(Usuario usuario);
        Task<SubmitResult<Usuario>> Editar(Guid id, Usuario usuario);
        Task<SubmitResult<Usuario>> Remover(Guid id);
        Task<bool> Existe(Guid id);
    }
}
