using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootProjeto;
using FluentValidation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootGrupo.Validators
{
    public class AvaliarGrupoValidator : AbstractValidator<Grupo>
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly Guid _avaliadorId;

        public AvaliarGrupoValidator(IGrupoRepository grupoRepository, IAvaliacaoRepository avaliacaoRepository, Guid avaliadorId)
        {
            _avaliadorId = avaliadorId;
            _grupoRepository = grupoRepository;
            _avaliacaoRepository = avaliacaoRepository;

            RuleFor(g => g.Id)
                .MustAsync(SerUmAvaliador).WithMessage("Não está autorizado à avaliar esse grupo.")
                .MustAsync(AvaliacaoNaoRealizada).WithMessage("Avaliação do grupo já foi realizada.");

            RuleFor(g => g.Projeto.Estado)
                .NotEqual(Projeto.EnumEstado.Elaboracao).WithMessage("A avaliação deste grupo ainda não está disponível.")
                .NotEqual(Projeto.EnumEstado.Encerrado).WithMessage("A avaliação deste grupo não está mais disponível.");
        }

        private async Task<bool> AvaliacaoNaoRealizada(Guid id, CancellationToken token)
        {
            return !(await _avaliacaoRepository.ObterAvaliacoesPorGrupo(id))
                .Any(x => x.AvaliadorId == _avaliadorId);
        }

        private async Task<bool> SerUmAvaliador(Guid id, CancellationToken token)
        {
            return (await _grupoRepository.ObterAvaliadores(id)).Any(x => x.Id == _avaliadorId);
        }
    }
}
