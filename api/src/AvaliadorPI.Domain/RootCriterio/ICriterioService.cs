using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootCriterio
{
    public interface ICriterioService
    {
        Task<GridResult> ObterTodosPorProjeto(Guid projetoId, GridRequest request = null);
        Task<Criterio> ObterPorId(Guid id);
        Task<SubmitResult<Criterio>> Cadastrar(Criterio criterio);
        Task<SubmitResult<Criterio>> Editar(Guid id, Criterio criterio);
        Task<SubmitResult<Criterio>> Remove(Guid id);
        Task<bool> Existe(Guid id);
    }
}
