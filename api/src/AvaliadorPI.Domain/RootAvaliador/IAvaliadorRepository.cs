using AvaliadorPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliador
{
    public interface IAvaliadorRepository : IRepository<Avaliador>
    {
        Task<IEnumerable<Avaliador>> ObterPorProjetoAtivo(Guid alunoId);
    }
}
