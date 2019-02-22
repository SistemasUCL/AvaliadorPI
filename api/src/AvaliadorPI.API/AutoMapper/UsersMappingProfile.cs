
using AutoMapper;
using AvaliadorPI.API.ViewModels.Shared;
using AvaliadorPI.Domain.RootAdministrador;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootUsuario;

namespace AvaliadorPI.API.AutoMapper
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<Professor, UsuarioViewModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<Aluno, UsuarioViewModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<Avaliador, UsuarioViewModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<Administrador, UsuarioViewModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<Usuario, UsuarioViewModel>();

            CreateMap<UsuarioViewModel, Usuario>();
        }
    }

}
