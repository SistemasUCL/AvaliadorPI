using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Aluno
{
    public class ResultadoAvaliacaoViewModel
    {
        public Guid AlunoId { get; set; }
        public Guid GrupoId { get; set; }
        public Guid AvaliadorId { get; set; }

        public IEnumerable<AvaliacaoCriterioViewModel> AvaliacoesCriterios { get; set; }

        public ResultadoAvaliacaoViewModel()
        {
            AvaliacoesCriterios = new List<AvaliacaoCriterioViewModel>();
        }
    }

    public class AvaliacaoCriterioViewModel
    {
        public Guid CriterioId { get; set; }
        public int Nota { get; set; }
    }
}





