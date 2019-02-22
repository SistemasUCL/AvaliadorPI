using AvaliadorPI.Data.Configurations;
using AvaliadorPI.Domain.RootAdministrador;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;
using AvaliadorPI.Domain.RootUsuario;
using Microsoft.EntityFrameworkCore;

namespace AvaliadorPI.Data.Context
{
    public class AvaliadorPIContext : DbContext
    {
        public AvaliadorPIContext(DbContextOptions<AvaliadorPIContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Avaliador> Avaliadores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Criterio> Criterios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdministradorConfiguration());
            modelBuilder.ApplyConfiguration(new AlunoConfiguration());
            modelBuilder.ApplyConfiguration(new AssociacaoAlunoGrupoConfiguration());
            modelBuilder.ApplyConfiguration(new AssociacaoDisciplinaProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new AssociacaoDisciplinaProjetoConfiguration());
            modelBuilder.ApplyConfiguration(new AssociacaoAvaliadorProjetoConfiguration());
            modelBuilder.ApplyConfiguration(new AvaliadorConfiguration());
            modelBuilder.ApplyConfiguration(new AvaliacaoConfiguration());
            modelBuilder.ApplyConfiguration(new CriterioConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplinaConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
