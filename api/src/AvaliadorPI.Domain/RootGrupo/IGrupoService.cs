using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootGrupo
{
    public interface IGrupoService
    {
        Task<GridResult> ObterDadosGrid(GridRequest request = null);
        Task<Grupo> ObterPorId(Guid id);
        Task<SubmitResult<Grupo>> Cadastrar(Grupo grupo);
        Task<SubmitResult<Grupo>> Editar(Guid id, Grupo grupo);
        Task<SubmitResult<Grupo>> Remover(Guid id);
        Task<SubmitResult<Grupo>> AssociarAluno(Guid grupoId, Guid alunoId);
        Task<SubmitResult<Grupo>> RemoverAluno(Guid grupoId, Guid alunoId);
        Task<GridResult> ObterDadosGridAlunos(Guid grupoId, GridRequest request);
        Task<bool> Existe(Guid id);
        Task<AvaliacaoGrupo> ObterAvaliacaoParaGrupo(Guid id);
        Task<ValidationResult> ValidarAvaliador(Guid grupoId, Guid avaliadorId);
        Task<GridResult> ObterDadosAvaliacoes(Guid id, GridRequest request = null);
    }
}
