using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliador
{
    public interface IAvaliadorService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Avaliador> ObterPorId(Guid id);
        Task<SubmitResult<Avaliador>> Cadastrar(Avaliador avaliador);
        Task<SubmitResult<Avaliador>> Remover(Guid id);
        Task<bool> Existe(Guid id);
    }
}
