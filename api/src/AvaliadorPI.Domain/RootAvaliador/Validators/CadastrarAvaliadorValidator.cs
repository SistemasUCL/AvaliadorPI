using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliador.Validators
{
    public class CadastrarAvaliadorValidator : AbstractValidator<Avaliador>
    {
        private readonly IAvaliadorRepository _AvaliadorRepository;

        public CadastrarAvaliadorValidator(IAvaliadorRepository AvaliadorRepository)
        {
            _AvaliadorRepository = AvaliadorRepository;

            RuleFor(c => c.Id)
                .MustAsync(AvaliadorUnico).WithMessage("Este usuario já está cadastrado como avaliador!");
        }

        private async Task<bool> AvaliadorUnico(Guid id, CancellationToken token)
        {
            return !await _AvaliadorRepository.AnyAsync(x => x.Id == id);
        }

    }
}
