using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProfessor.Validators
{
    public class ExcluirProfessorValidator : AbstractValidator<Professor>
    {
        private readonly IProfessorRepository _professorRepository;

        public ExcluirProfessorValidator(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;

            RuleFor(c => c.Id)
                .MustAsync(ProfessorNaoAssociadoProjeto).WithMessage("Esta disciplina está associada a um projeto e não pode ser excluida");
        }

        private async Task<bool> ProfessorNaoAssociadoProjeto(Guid id, CancellationToken token)
        {
            return !await _professorRepository.TemAssociacaoProjeto(id);
        }
    }
}
