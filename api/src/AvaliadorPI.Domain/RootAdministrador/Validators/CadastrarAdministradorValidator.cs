using AvaliadorPI.Domain.RootAdministrador;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAdministrador.Validators
{
    public class CadastrarAdministradorValidator : AbstractValidator<Administrador>
    {
        private readonly IAdministradorRepository _AdministradorRepository;

        public CadastrarAdministradorValidator(IAdministradorRepository AdministradorRepository)
        {
            _AdministradorRepository = AdministradorRepository;

            RuleFor(c => c.Id)
                .MustAsync(AlunoUnico).WithMessage("Este usuario já está cadastrado como administrador!");
        }

        private async Task<bool> AlunoUnico(Guid id, CancellationToken token)
        {
            return !await _AdministradorRepository.AnyAsync(x => x.Id == id);
        }

    }
}
