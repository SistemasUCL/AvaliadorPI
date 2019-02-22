using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootProjeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Projeto> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Professor.Usuario)
                .Include(x => x.Criterios)
                .Include(x => x.Grupos)
                .Include(x => x.AssociacaoAvaliadorProjeto)
                    .ThenInclude(a => a.Avaliador.Usuario)
                .Include(x => x.AssociacaoDisciplinaProjeto)
                    .ThenInclude(a => a.Disciplina)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Projeto>> SearchAsync(Expression<Func<Projeto, bool>> predicate)
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.Professor.Usuario)
                .Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Projeto>> GetAllAsync()
        {
            return await DbSet.AsNoTracking()
                .Include(x => x.Professor.Usuario)
                .ToListAsync();
        }

        public async Task<bool> TemAssociacaoGrupo(Guid id)
        {
            return await Db.Grupos.AnyAsync(x => x.ProjetoId == id);
        }

        public async Task<bool> TemAssociacaoCriterio(Guid id)
        {
            return await Db.Criterios.AnyAsync(x => x.ProjetoId == id);
        }

        public async Task<Projeto> ObterProjetoParaAvaliacao(Guid id)
        {
            return await DbSet
                .Include(x => x.Criterios)
                .Include(x => x.AssociacaoAvaliadorProjeto)
                        .ThenInclude(a => a.Avaliador)
                .Include(x => x.Grupos)
                    .ThenInclude(a => a.AssociacaoAlunoGrupo)
                        .ThenInclude(a => a.Aluno.Usuario)
               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Projeto> ObterProjetoAtivoPorAluno(Guid alunoId)
        {
            return await DbSet
                .Include(x => x.Criterios)
                .Include(x => x.AssociacaoAvaliadorProjeto)
                    .ThenInclude(x => x.Avaliador.Usuario)
                .FirstOrDefaultAsync(projeto =>
                    projeto.Estado == Projeto.EnumEstado.Avaliacao &&
                    projeto.Grupos.Any(grupo => grupo.AssociacaoAlunoGrupo.Any(a => a.AlunoId == alunoId)));
        }
    }
}
