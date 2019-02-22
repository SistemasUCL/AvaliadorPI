using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAluno
{
    public interface IAlunoService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Aluno> ObterPorId(Guid id);
        Task<SubmitResult<Aluno>> Cadastrar(Aluno aluno);
        Task<SubmitResult<Aluno>> Editar(Guid id, Aluno aluno);
        Task<SubmitResult<Aluno>> Remover(Guid id);
        Task<bool> Existe(Guid id);
        Task<GridResult> ObterDadosAvaliacoes(Guid id, GridRequest request = null);
        Task<AvaliacaoAluno> ObterAvaliacao(Guid id);
    }
}
