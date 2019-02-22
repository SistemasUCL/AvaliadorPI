using AutoMapper;
using AvaliadorPI.API.ViewModels.Avaliador;
using AvaliadorPI.Domain.RootAvaliador;

namespace AvaliadorPI.API.AutoMapper
{
    public class AvaliadorMappingProfile : Profile
    {
        public AvaliadorMappingProfile()
        {
            CreateMap<Avaliador, AvaliadorViewModel>()
                .ForMember(x => x.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<AvaliadorFormViewModel, Avaliador>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.UsuarioId));
        }
    }

}
