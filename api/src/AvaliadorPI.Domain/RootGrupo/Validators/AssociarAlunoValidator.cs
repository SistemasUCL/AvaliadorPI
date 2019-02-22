using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootGrupo.Validators
{
    public class AssociarAlunoValidator : AbstractValidator<Grupo>
    {
        private readonly IGrupoRepository _grupoRepository;

        public AssociarAlunoValidator(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;

            RuleFor(c => c.AssociacaoAlunoGrupo.Select(x => x.AlunoId))
                .Must(x => !x.GroupBy(id => id).Any(g => g.Count() > 1)).WithMessage("Este aluno já pertence às este grupo!");

            RuleFor(g => g)
                .MustAsync(AlunoPertenceUmUnicoGrupo).WithMessage("Este aluno já pertence à outro grupo");
        }

        private async Task<bool> AlunoPertenceUmUnicoGrupo(Grupo grupo, CancellationToken token)
        {
            if (grupo.AssociacaoAlunoGrupo == null || grupo.AssociacaoAlunoGrupo.Count() == 0) return true;

            var ids = grupo.AssociacaoAlunoGrupo.Select(a => a.AlunoId).ToList();

            return !await _grupoRepository.AnyAsync(g => g.Id != grupo.Id
                && g.ProjetoId == grupo.ProjetoId
                && g.AssociacaoAlunoGrupo.Any(a => ids.Contains(a.AlunoId)));
        }
    }
}
