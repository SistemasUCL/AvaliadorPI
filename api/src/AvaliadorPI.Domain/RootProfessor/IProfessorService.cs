using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProfessor
{
    public interface IProfessorService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Professor> ObterPorId(Guid id);
        Task<SubmitResult<Professor>> Cadastrar(Professor professor);
        Task<SubmitResult<Professor>> Remover(Guid id);
        Task<SubmitResult<Professor>> Editar(Guid id, Professor professor);
        Task<bool> Existe(Guid id);
    }
}
