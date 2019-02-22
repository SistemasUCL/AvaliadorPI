using AvaliadorPI.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootDisciplina
{
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<bool> TemAssociacaoProjeto(Guid id);
    }
}
