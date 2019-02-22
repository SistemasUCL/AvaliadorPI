using AutoMapper;
using AvaliadorPI.API.ViewModels.Disciplina;
using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;

namespace AvaliadorPI.API.AutoMapper
{
    public class DisciplinaMappingProfile : Profile
    {
        public DisciplinaMappingProfile()
        {
            CreateMap<Projeto, ProjetoViewModel>();

            CreateMap<Professor, ProfessorViewModel>()
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Disciplina, DisciplinaViewModel>();

            CreateMap<DisciplinaFormViewModel, Disciplina>();
        }
    }

}
