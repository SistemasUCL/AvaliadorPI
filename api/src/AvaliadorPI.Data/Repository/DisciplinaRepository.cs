using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootDisciplina;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Disciplina> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.AssociacaoDisciplinaProfessor)
                    .ThenInclude(a => a.Professor.Usuario)
                .Include(x => x.AssociacaoDisciplinaProjeto)
                    .ThenInclude(a => a.Projeto)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Disciplina>> SearchAsync(Expression<Func<Disciplina, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.AssociacaoDisciplinaProfessor)
                .Include(x => x.AssociacaoDisciplinaProjeto)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Disciplina>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.AssociacaoDisciplinaProfessor)
                .Include(x => x.AssociacaoDisciplinaProjeto)
                .AsNoTracking().ToListAsync();
        }

        public async Task<bool> TemAssociacaoProjeto(Guid id)
        {
            return await Db.Projetos
                .SelectMany(x => x.AssociacaoDisciplinaProjeto)
                .AnyAsync(x => x.DisciplinaId == id);
        }
    }
}
