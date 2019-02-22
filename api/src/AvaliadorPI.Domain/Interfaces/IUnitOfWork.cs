using System;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync();
    }
}
