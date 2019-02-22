using FluentValidation;
using System.Linq;

namespace AvaliadorPI.Domain.RootProjeto.Validators
{
    public class AssociarAvaliadorValidator : AbstractValidator<Projeto>
    {
        public AssociarAvaliadorValidator()
        {
            RuleFor(c => c.AssociacaoAvaliadorProjeto.Select(x => x.AvaliadorId))
                .Must(x => !x.GroupBy(id => id).Any(g => g.Count() > 1)).WithMessage("Não podem haver avaliadores duplicados!");
        }
    }
}
