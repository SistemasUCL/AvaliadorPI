using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProjeto.Validators
{
    public class ExcluirProjetoValidator : AbstractValidator<Projeto>
    {
        private readonly IProjetoRepository _projetoRepository;

        public ExcluirProjetoValidator(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;

            RuleFor(c => c.Id)
                .MustAsync(ProjetoNaoAssociadoCriterio).WithMessage("Este projeto está associado à um critério e não pode ser excluído")
                .MustAsync(ProjetoNaoAssociadoGrupo).WithMessage("Este projeto está associado à um grupo e não pode ser excluído");
        }

        private async Task<bool> ProjetoNaoAssociadoCriterio(Guid id, CancellationToken token)
        {
            return !await _projetoRepository.TemAssociacaoCriterio(id);
        }

        private async Task<bool> ProjetoNaoAssociadoGrupo(Guid id, CancellationToken token)
        {
            return !await _projetoRepository.TemAssociacaoGrupo(id);
        }
    }
}
