using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootDisciplina.Validators
{
    public class ExcluirDisciplinaValidator : AbstractValidator<Disciplina>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public ExcluirDisciplinaValidator(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;

            RuleFor(c => c.Id)
                .MustAsync(DisciplinaNaoAssociadaProjeto).WithMessage("Esta disciplina está associada a um projeto e não pode ser excluida");
        }

        private async Task<bool> DisciplinaNaoAssociadaProjeto(Guid id, CancellationToken token)
        {
            return !await _disciplinaRepository.TemAssociacaoProjeto(id);
        }
    }
}
