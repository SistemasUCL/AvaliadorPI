using AutoMapper;
using AvaliadorPI.API.ViewModels.Shared;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;
using AvaliadorPI.Domain.RootUsuario;

namespace AvaliadorPI.API.AutoMapper
{
    public class SelectsMappingProfile : Profile
    {
        public SelectsMappingProfile()
        {
            CreateMap<Grupo, SelectBoxViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Projeto, SelectBoxViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Periodo + src.Tema));

            CreateMap<Professor, SelectBoxViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Aluno, SelectBoxViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Avaliador, SelectBoxViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Usuario, SelectBoxViewModel>()
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(src => src.Nome));
        }
    }

}
