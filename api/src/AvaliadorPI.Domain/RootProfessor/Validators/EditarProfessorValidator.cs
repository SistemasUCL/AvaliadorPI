using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProfessor.Validators
{
    public class EditarProfessorValidator : AbstractValidator<Professor>
    {
        private readonly IProfessorRepository _ProfessorRepository;

        public EditarProfessorValidator(IProfessorRepository ProfessorRepository)
        {
            _ProfessorRepository = ProfessorRepository;

            RuleFor(c => c.Matricula)
                .NotEmpty().WithMessage("A matricula não pode ser vazia")
                .MaximumLength(50).WithMessage("A matricula não pode ter mais que 15 caracteres")
                .MustAsync(MatriculaUnica).WithMessage("Já existe um professor cadastrado com essa matrícula!");
        }

        public async Task<bool> MatriculaUnica(Professor professor, string matricula, CancellationToken token)
        {
            return !await _ProfessorRepository.AnyAsync(x => x.Matricula == matricula && x.Id != professor.Id);
        }
    }
}
