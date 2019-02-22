using AvaliadorPI.Domain.RootProfessor;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootProfessor.Validators
{
    public class CadastrarProfessorValidator : AbstractValidator<Professor>
    {
        private readonly IProfessorRepository _ProfessorRepository;

        public CadastrarProfessorValidator(IProfessorRepository ProfessorRepository)
        {
            _ProfessorRepository = ProfessorRepository;

            RuleFor(c => c.Id)
                .MustAsync(ProfessorNaoCadastradoAinda).WithMessage("Este usuario já está cadastrado como professor!");

            RuleFor(c => c.Matricula)
                .NotEmpty().WithMessage("A matricula não pode ser vazia")
                .MaximumLength(50).WithMessage("A matricula não pode ter mais que 15 caracteres")
                .MustAsync(MatriculaUnica).WithMessage("Já existe um professor cadastrado com essa matrícula!");
        }

        private async Task<bool> ProfessorNaoCadastradoAinda(Guid id, CancellationToken token)
        {
            return !await _ProfessorRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> MatriculaUnica(string matricula, CancellationToken token)
        {
            return !await _ProfessorRepository.AnyAsync(x => x.Matricula == matricula);
        }
    }
}
