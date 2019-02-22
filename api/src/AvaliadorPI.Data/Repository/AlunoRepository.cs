using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootAluno;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Aluno> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .Include(x => x.AssociacaoAlunoGrupo)
                    .ThenInclude(a => a.Grupo)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Aluno>> SearchAsync(Expression<Func<Aluno, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Aluno>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().ToListAsync();
        }
    }
}
