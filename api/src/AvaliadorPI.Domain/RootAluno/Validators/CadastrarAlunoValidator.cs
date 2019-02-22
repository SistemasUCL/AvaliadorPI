using AvaliadorPI.Domain.RootAluno;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAluno.Validators
{
    public class CadastrarAlunoValidator : AbstractValidator<Aluno>
    {
        private readonly IAlunoRepository _AlunoRepository;
        public CadastrarAlunoValidator(IAlunoRepository AlunoRepository)
        {
            _AlunoRepository = AlunoRepository;

            RuleFor(c => c.Id)
                .MustAsync(AlunoNaoCadastradoAinda).WithMessage("Este usuario já está cadastrado como aluno!");

            RuleFor(c => c.Matricula)
                .NotEmpty().WithMessage("A matricula não pode ser vazia")
                .MaximumLength(50).WithMessage("A matricula não pode ter mais que 15 caracteres");
        }

        private async Task<bool> AlunoNaoCadastradoAinda(Guid id, CancellationToken token)
        {
            return !await _AlunoRepository.AnyAsync(x => x.Id == id);
        }
    }
}
