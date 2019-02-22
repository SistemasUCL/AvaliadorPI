using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootDisciplina
{
    public interface IDisciplinaService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Disciplina> ObterPorId(Guid id);
        Task<SubmitResult<Disciplina>> Cadastrar(Disciplina disciplina);
        Task<SubmitResult<Disciplina>> Editar(Guid id, Disciplina disciplina);
        Task<SubmitResult<Disciplina>> Remover(Guid id);
        Task<bool> Existe(Guid id);
    }
}
