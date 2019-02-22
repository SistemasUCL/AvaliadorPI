using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootAvaliador;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootGrupo
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        Task<Grupo> ObterGrupoParaAvaliacao(Guid id);
        Task<Grupo> ObterGrupo(Guid alunoId, Guid projetoId);
        Task<IEnumerable<Avaliador>> ObterAvaliadores(Guid grupoId);
    }
}
