using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAdministrador
{
    public interface IAdministradorService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Administrador> ObterPorId(Guid id);
        Task<SubmitResult<Administrador>> Cadastrar(Administrador administrador);
        Task<SubmitResult<Administrador>> Remover(Guid id);
        Task<bool> Existe(Guid id);
    }
}
