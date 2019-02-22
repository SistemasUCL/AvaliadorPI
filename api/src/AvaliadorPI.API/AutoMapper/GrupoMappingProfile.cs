using AutoMapper;
using AvaliadorPI.API.ViewModels.Grupo;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;

namespace AvaliadorPI.API.AutoMapper
{
    public class GrupoMappingProfile : Profile
    {
        public GrupoMappingProfile()
        {
            CreateMap<Grupo, GrupoViewModel>();
            CreateMap<Projeto, ProjetoViewModel>();
            CreateMap<Criterio, CriterioViewModel>();

            CreateMap<Professor, ProfessorViewModel>()
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Aluno, AlunoViewModel>()
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome));

            CreateMap<GrupoFormViewModel, Grupo>();
        }
    }

}
