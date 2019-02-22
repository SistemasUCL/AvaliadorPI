using AutoMapper;
using AvaliadorPI.API.ViewModels.Administrador;
using AvaliadorPI.Domain.RootAdministrador;

namespace AvaliadorPI.API.AutoMapper
{
    public class AdministradorMappingProfile : Profile
    {
        public AdministradorMappingProfile()
        {
            CreateMap<Administrador, AdministradorViewModel>()
                .ForMember(x => x.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<AdministradorFormViewModel, Administrador>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.UsuarioId)); ;
        }
    }

}
