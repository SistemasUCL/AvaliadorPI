using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootAvaliacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AvaliadorPI.Data.Repository
{
    public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
    {
        public AvaliacaoRepository(AvaliadorPIContext context) : base(context) { }

        public override async Task<Avaliacao> GetByIdAsync(Guid id)
        {
            return await DbSet
                .Include(x => x.Aluno)
                .Include(x => x.Grupo)
                .Include(x => x.Avaliador)
                .Include(x => x.Criterio)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<Avaliacao>> SearchAsync(Expression<Func<Avaliacao, bool>> predicate)
        {
            return await DbSet
                .Include(x => x.Aluno)
                .Include(x => x.Grupo)
                .Include(x => x.Avaliador)
                .Include(x => x.Criterio)
                .AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Avaliacao>> GetAllAsync()
        {
            return await DbSet
                .Include(x => x.Aluno)
                .Include(x => x.Grupo)
                .Include(x => x.Avaliador)
                .Include(x => x.Criterio)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AvaliacaoCalculavel>> ObterAvaliacoesPorProjeto(Guid projetoId)
        {
            return await DbSet
                .Where(x => x.Grupo.ProjetoId == projetoId)
                .Select(x => new AvaliacaoCalculavel
                {
                    Aluno = x.Aluno.Usuario.NomeCompleto,
                    Grupo = x.Grupo.Nome,
                    Nota = x.Nota,
                    Peso = x.Criterio.Peso,
                    AvaliadorId = x.AvaliadorId,
                })
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AvaliacaoCalculavel>> ObterAvaliacoesPorGrupo(Guid grupoId)
        {
            return await DbSet
                .Where(x => x.GrupoId == grupoId)
                .Select(x => new AvaliacaoCalculavel
                {
                    Criterio = x.Criterio.Titulo,
                    Nota = x.Nota,
                    Peso = x.Criterio.Peso,
                    AvaliadorId = x.AvaliadorId,
                })
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AvaliacaoCalculavel>> ObterAvaliacoesPorAluno(Guid alunoId, Guid grupoId)
        {
            return await DbSet
                .Where(x => (x.AlunoId == alunoId && x.GrupoId == grupoId))
                .Select(x => new AvaliacaoCalculavel
                {
                    Criterio = x.Criterio.Titulo,
                    Nota = x.Nota,
                    Peso = x.Criterio.Peso,
                    AvaliadorId = x.AvaliadorId,
                })
                .AsNoTracking().ToListAsync();
        }

        public async Task AddOrUpdateAvaliacaoAluno(Avaliacao avaliacao)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x =>
                        x.CriterioId == avaliacao.CriterioId &&
                        x.AlunoId == avaliacao.AlunoId &&
                        x.GrupoId == avaliacao.GrupoId &&
                        x.AvaliadorId == avaliacao.AvaliadorId);

            if (entity == null)
            {
                Add(avaliacao);
            }
            else
            {
                entity.Nota = avaliacao.Nota;
                entity.Tipo = avaliacao.Tipo;
            }
        }
    }
}
