using FluentValidation;

namespace AvaliadorPI.Domain.RootAluno.Validators
{
    public class EditarAlunoValidator : AbstractValidator<Aluno>
    {
        public EditarAlunoValidator()
        {
            RuleFor(c => c.Matricula)
                .NotEmpty().WithMessage("A matricula não pode ser vazia")
                .MaximumLength(50).WithMessage("A matricula não pode ter mais que 15 caracteres");
        }
    }
}
