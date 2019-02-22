using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootAdministrador;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class AdministradorRepository : Repository<Administrador>, IAdministradorRepository
    {
        public AdministradorRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Administrador> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Administrador>> SearchAsync(Expression<Func<Administrador, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().ToListAsync();
        }
    }
}
