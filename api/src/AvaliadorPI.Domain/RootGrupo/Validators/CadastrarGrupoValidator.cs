using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootGrupo.Validators
{
    public class CadastrarGrupoValidator : AbstractValidator<Grupo>
    {
        private readonly IGrupoRepository _GrupoRepository;

        public CadastrarGrupoValidator(IGrupoRepository GrupoRepository)
        {
            _GrupoRepository = GrupoRepository;

            RuleFor(c => c.ProjetoId)
                .NotEmpty().WithMessage("O grupo precisa estar vinculado à um projeto!");

            RuleFor(c => c.NomeProjeto)
                .NotEmpty().WithMessage("O campo Nome do Projeto não pode ser vazio")
                .MaximumLength(100).WithMessage("O campo Nome do Projeto não pode ter mais de {MaxLength} caracteres");

            RuleFor(c => c.Descricao)
                .MaximumLength(256).WithMessage("O campo Descrição não pode ter mais de {MaxLength} caracteres");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo Nome não pode ser vazio")
                .MaximumLength(100).WithMessage("O campo Nome não pode ter mais de {MaxLength} caracteres")
                .MustAsync(NomeGrupoUnico).WithMessage("Nome de grupo já existe");
        }

        private async Task<bool> NomeGrupoUnico(string nome, CancellationToken token)
        {
            return !await _GrupoRepository.AnyAsync(x => x.Nome == nome);
        }
    }
}
