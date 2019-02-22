using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootDisciplina.Validators
{
    public class CadastrarDisciplinaValidator : AbstractValidator<Disciplina>
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public CadastrarDisciplinaValidator(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do Disciplina não pode ser vazio")
                .MustAsync(DisciplinaNomeUnico).WithMessage("Já existe uma disciplian com esse nome!");
        }

        private async Task<bool> DisciplinaNomeUnico(string nome, CancellationToken token)
        {
            return !await _disciplinaRepository.AnyAsync(x => x.Nome == nome);
        }
    }
}
