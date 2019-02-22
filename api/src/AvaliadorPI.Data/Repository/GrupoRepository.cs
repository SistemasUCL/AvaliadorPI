using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootGrupo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class GrupoRepository : Repository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Grupo> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Projeto)
                .Include(x => x.AssociacaoAlunoGrupo)
                    .ThenInclude(a => a.Aluno.Usuario)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Grupo>> SearchAsync(Expression<Func<Grupo, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Projeto)
                .Include(x => x.AssociacaoAlunoGrupo)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Grupo>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Projeto)
                .Include(x => x.AssociacaoAlunoGrupo)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Grupo> ObterGrupoParaAvaliacao(Guid id)
        {
            return await DbSet
                .Include(x => x.Projeto.Criterios)
                .Include(x => x.AssociacaoAlunoGrupo)
                    .ThenInclude(a => a.Aluno.Usuario)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Grupo> ObterGrupo(Guid alunoId, Guid projetoId)
        {
            return await DbSet
                .Include(x => x.AssociacaoAlunoGrupo)
                    .ThenInclude(a => a.Aluno.Usuario)
                .SingleOrDefaultAsync(x => x.ProjetoId == projetoId && x.AssociacaoAlunoGrupo.Any(y => y.AlunoId == alunoId));
        }

        public async Task<IEnumerable<Avaliador>> ObterAvaliadores(Guid grupoId)
        {
            var grupo = await DbSet
                .Include(x => x.Projeto.AssociacaoAvaliadorProjeto)
                    .ThenInclude(x => x.Avaliador)
                .FirstOrDefaultAsync(x => x.Id == grupoId);

            return grupo.Projeto.AssociacaoAvaliadorProjeto.Select(x => x.Avaliador);
        }
    }
}
