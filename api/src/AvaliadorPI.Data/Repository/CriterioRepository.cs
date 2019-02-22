using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootProjeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class CriterioRepository : Repository<Criterio>, ICriterioRepository
    {
        public CriterioRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Criterio> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Projeto)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Criterio>> SearchAsync(Expression<Func<Criterio, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Projeto)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Criterio>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Projeto)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Criterio>> ObterPorProjetoAtivo(Guid alunoId)
        {
            return await DbSet
                .Where(x => x.Projeto.Estado == Projeto.EnumEstado.Avaliacao &&
                    x.Projeto.Grupos.Any(grupo => grupo.AssociacaoAlunoGrupo.Any(a => a.AlunoId == alunoId)))
                .AsNoTracking().ToListAsync();
        }
    }
}
