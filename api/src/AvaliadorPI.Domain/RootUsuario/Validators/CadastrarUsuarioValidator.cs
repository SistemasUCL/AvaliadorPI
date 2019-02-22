using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootUsuario.Validators
{
    public class CadastrarUsuarioValidator : AbstractValidator<Usuario>
    {
        private readonly IUsuarioRepository _UsuarioRepository;

        public CadastrarUsuarioValidator(IUsuarioRepository UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do usuario não pode ser vazio")
                .MaximumLength(50).WithMessage("O nome do usuario não pode ter mais que 50 caracteres");

            RuleFor(c => c.SobreNome)
                .MaximumLength(50).WithMessage("O sobrenome do usuario não pode ter mais que 50 caracteres");

            RuleFor(c => c.Telefone)
                //.NotEmpty().WithMessage("O telefone do usuario não pode ser vazio")
                .MaximumLength(50).WithMessage("O nome do usuario não pode ter mais que 20 caracteres"); ;

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email do usuario não pode ser vazio")
                .EmailAddress().WithMessage("O email não é válido")
                .MaximumLength(50).WithMessage("O nome do usuario não pode ter mais que 100 caracteres")
                .MustAsync(EmailUnico).WithMessage("Já existe usuario cadastrado com esse e-mail");
        }

        private async Task<bool> EmailUnico(string email, CancellationToken token)
        {
            return !await _UsuarioRepository.AnyAsync(x => x.Email == email);
        }
    }
}
