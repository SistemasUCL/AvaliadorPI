using AutoMapper;
using AvaliadorPI.API.ViewModels.Projeto;
using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;
using System.Linq;

namespace AvaliadorPI.API.AutoMapper
{
    public class ProjetoMappingProfile : Profile
    {
        public ProjetoMappingProfile()
        {
            CreateMap<Projeto, ProjetoViewModel>()
                .ForMember(x => x.Disciplinas, opt => opt.MapFrom(src => src.AssociacaoDisciplinaProjeto.Select(x => x.Disciplina).Select(x => x.Id)));

            CreateMap<Professor, ProfessorViewModel>()
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Avaliador, AvaliadorViewModel>()
                .ForMember(x => x.Nome, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Criterio, CriterioViewModel>();
            CreateMap<Disciplina, DisciplinaViewModel>();
            CreateMap<CriterioFormViewModel, Criterio>();

            CreateMap<ProjetoFormViewModel, Projeto>()
                .ForMember(vm => vm.AssociacaoDisciplinaProjeto,
                opt => opt.MapFrom(e => e.Disciplinas.Select(id => new AssociacaoDisciplinaProjeto { DisciplinaId = id })));
        }
    }
}