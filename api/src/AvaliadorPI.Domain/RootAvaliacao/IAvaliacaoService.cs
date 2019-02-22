using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public interface IAvaliacaoService
    {
        Task<IEnumerable<Avaliacao>> ObterDadosGrid(GridRequest request = null);
        Task<Avaliacao> ObterPorId(Guid id);
        Task<SubmitResult<Avaliacao>> Editar(Guid id, int nota);
        Task<ValidationResult> RealizarAvaliacao(Guid id, IEnumerable<Avaliacao> avaliacoes);
        Task<bool> Existe(Guid id);
        Task<ValidationResult> RealizarAvaliacaoAluno(List<Avaliacao> avaliacoes);
    }
}
