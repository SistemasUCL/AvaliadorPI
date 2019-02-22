using System.Collections.Generic;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public class ResultadoAvaliacaoCriterio
    {
        public ResultadoAvaliacaoCriterio()
        {
            Notas = new List<double?>();
        }

        public string Criterio { get; set; }
        public double? MediaFinal { get; set; }

        public List<double?> Notas { get; set; }
    }
}
