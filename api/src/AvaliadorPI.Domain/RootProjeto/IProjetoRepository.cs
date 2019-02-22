using AvaliadorPI.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProjeto
{
    public interface IProjetoRepository : IRepository<Projeto>
    {
        Task<bool> TemAssociacaoCriterio(Guid id);
        Task<bool> TemAssociacaoGrupo(Guid id);
        Task<Projeto> ObterProjetoParaAvaliacao(Guid id);
        Task<Projeto> ObterProjetoAtivoPorAluno(Guid alunoId);
    }
}
