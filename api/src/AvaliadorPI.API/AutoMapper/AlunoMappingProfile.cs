using AutoMapper;
using AvaliadorPI.API.ViewModels.Aluno;
using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootGrupo;

namespace AvaliadorPI.API.AutoMapper
{
    public class AlunoMappingProfile : Profile
    {
        public AlunoMappingProfile()
        {
            CreateMap<Grupo, GrupoViewModel>();
            CreateMap<Criterio, CriterioViewModel>();
            CreateMap<AvaliacaoAluno, AvaliacaoAlunoViewModel>();

            CreateMap<Avaliador, AvaliadorViewModel>()
                .ForMember(x => x.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.NomeCompleto));

            CreateMap<Aluno, AlunoViewModel>()
                .ForMember(x => x.UsuarioId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Usuario.Email))
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome))
                .ForMember(x => x.SobreNome, opt => opt.MapFrom(src => src.Usuario.SobreNome))
                .ForMember(x => x.Telefone, opt => opt.MapFrom(src => src.Usuario.Telefone));

            CreateMap<AlunoFormViewModel, Aluno>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.UsuarioId));
        }
    }

}
