using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProjeto
{
    public interface IProjetoService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Projeto> ObterPorId(Guid id);
        Task<SubmitResult<Projeto>> Cadastrar(Projeto projeto);
        Task<SubmitResult<Projeto>> Editar(Guid id, Projeto projeto);
        Task<SubmitResult<Projeto>> Remover(Guid id);
        Task<SubmitResult<Projeto>> AssociarAvaliador(Guid projetoId, Guid avaliadorId);
        Task<bool> Existe(Guid id);
        Task<SubmitResult<Projeto>> RemoverAvaliador(Guid projetoId, Guid id);
        Task<GridResult> ObterDadosGridAvaliadores(Guid projetoId, GridRequest request = null);
        Task<GridResult> ObterDadosGridGrupos(Guid projetoId, GridRequest request = null);
        Task<GridResult> ObterDadosAvaliacoes(Guid projetoId, GridRequest request = null);
        Task AtualizarEstado(Guid projetoId, int estado);
    }
}
