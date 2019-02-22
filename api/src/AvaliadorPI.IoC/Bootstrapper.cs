using AvaliadorPI.Data.Context;
using AvaliadorPI.Data.Repository;
using AvaliadorPI.Domain.Interfaces;
using AvaliadorPI.Domain.RootAdministrador;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;
using AvaliadorPI.Domain.RootUsuario;
using Microsoft.Extensions.DependencyInjection;

namespace AvaliadorPI.IoC
{
    public static class BootStrapper
    {
        public static void Register(IServiceCollection services)
        {
            // ASPNET
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IAvaliacaoService, AvaliacaoService>();
            services.AddScoped<IAvaliadorService, AvaliadorService>();
            services.AddScoped<ICriterioService, CriterioService>();
            services.AddScoped<IDisciplinaService, DisciplinaService>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Data
            services.AddScoped<AvaliadorPIContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdministradorRepository, AdministradorRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IAvaliadorRepository, AvaliadorRepository>();
            services.AddScoped<ICriterioRepository, CriterioRepository>();
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Infra - Filtros
            //services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            //services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
            //services.AddScoped<GlobalExceptionHandlingFilter>();
            //services.AddScoped<GlobalActionLogger>();
        }
    }
}
