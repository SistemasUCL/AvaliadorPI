using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootProfessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Professor> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .Include(x => x.Projetos)
                .Include(x => x.AssociacaoDisciplinaProfessor)
                    .ThenInclude(a => a.Disciplina)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Professor>> SearchAsync(Expression<Func<Professor, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().ToListAsync();
        }

        public async Task<bool> TemAssociacaoProjeto(Guid id)
        {
            return await Db.Projetos.AnyAsync(x => x.ProfessorId == id);
        }
    }
}
