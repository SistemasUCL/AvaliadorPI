using AutoMapper;
using AvaliadorPI.API.ViewModels.Professor;
using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;
using System.Linq;

namespace AvaliadorPI.API.AutoMapper
{
    public class ProfessorMappingProfile : Profile
    {
        public ProfessorMappingProfile()
        {
            CreateMap<Projeto, ProjetoViewModel>();
            CreateMap<Disciplina, DisciplinaViewModel>();

            CreateMap<Professor, ProfessorViewModel>()
                .ForMember(x => x.Disciplinas, opt => opt.MapFrom(src => src.AssociacaoDisciplinaProfessor.Select(x => x.Disciplina).Select(x => x.Id)))
                .ForMember(x => x.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<ProfessorFormViewModel, Professor>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.UsuarioId));
        }
    }
}
