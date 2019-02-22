using AvaliadorPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public interface IAvaliacaoRepository : IRepository<Avaliacao>
    {
        Task<IEnumerable<AvaliacaoCalculavel>> ObterAvaliacoesPorProjeto(Guid projetoId);
        Task<IEnumerable<AvaliacaoCalculavel>> ObterAvaliacoesPorGrupo(Guid grupoId);
        Task<IEnumerable<AvaliacaoCalculavel>> ObterAvaliacoesPorAluno(Guid alunoId, Guid grupoId);
        Task AddOrUpdateAvaliacaoAluno(Avaliacao avaliacao);
    }
}
