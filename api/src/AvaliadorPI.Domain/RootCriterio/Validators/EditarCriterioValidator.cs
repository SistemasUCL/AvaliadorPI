using FluentValidation;

namespace AvaliadorPI.Domain.RootCriterio.Validators
{
    public class EditarCriterioValidator : AbstractValidator<Criterio>
    {
        public EditarCriterioValidator()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("O título do Critério não pode ser vazio")
                .MaximumLength(100).WithMessage("O título do Critério não pode ter mais que 100 caracteres");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("A descrição do Critério não pode ser vazio")
                .MaximumLength(256).WithMessage("A descrição do Critério não pode ter mais que 256 caracteres");

            RuleFor(c => c.Peso)
                .GreaterThan(0).WithMessage("O peso deve ser um numero inteiro positivo")
                .LessThan(100).WithMessage("O peso deve ser menor que 100");

            RuleFor(c => c.Ordem)
                .GreaterThan(0).WithMessage("A ordem deve ser um numero inteiro positivo")
                .LessThan(100).WithMessage("A ordem deve ser menor que 100");
        }
    }
}
