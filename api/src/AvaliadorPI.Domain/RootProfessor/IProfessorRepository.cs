using AvaliadorPI.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProfessor
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<bool> TemAssociacaoProjeto(Guid id);
    }
}
