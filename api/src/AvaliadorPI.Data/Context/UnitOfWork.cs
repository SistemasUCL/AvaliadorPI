using AvaliadorPI.Domain.Interfaces;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AvaliadorPIContext _context;

        public UnitOfWork(AvaliadorPIContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
