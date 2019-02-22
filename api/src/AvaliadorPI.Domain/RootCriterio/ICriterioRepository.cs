using AvaliadorPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootCriterio
{
    public interface ICriterioRepository : IRepository<Criterio>
    {
        Task<IEnumerable<Criterio>> ObterPorProjetoAtivo(Guid alunoId);
    }
}
