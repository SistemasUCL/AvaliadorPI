using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProjeto.Validators
{
    public class CadastrarProjetoValidator : AbstractValidator<Projeto>
    {
        private readonly IProjetoRepository _ProjetoRepository;

        public CadastrarProjetoValidator(IProjetoRepository ProjetoRepository)
        {
            _ProjetoRepository = ProjetoRepository;

            RuleFor(c => c.Tema)
                .NotEmpty().WithMessage("O nome do Projeto não pode ser vazio")
                .MustAsync(NomeProjetoNaoExiste).WithMessage("Tema de Projeto já existe.");
        }

        private async Task<bool> NomeProjetoNaoExiste(string tema, CancellationToken token)
        {
            return !await _ProjetoRepository.AnyAsync(x => x.Tema == tema);
        }
    }
}
