using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootProjeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class AvaliadorRepository : Repository<Avaliador>, IAvaliadorRepository
    {
        public AvaliadorRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Avaliador> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Avaliador>> SearchAsync(Expression<Func<Avaliador, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Avaliador>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Usuario)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Avaliador>> ObterPorProjetoAtivo(Guid alunoId)
        {
            return await DbSet
                .Include(x => x.Usuario)
                .Where(x => x.AssociacaoAvaliadorProjeto.Any(y =>
                    y.Projeto.Estado == Projeto.EnumEstado.Avaliacao &&
                    y.Projeto.Grupos.Any(grupo => grupo.AssociacaoAlunoGrupo.Any(a => a.AlunoId == alunoId))))
                .AsNoTracking().ToListAsync();
        }
    }
}
