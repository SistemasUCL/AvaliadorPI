using System.Collections.Generic;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public class ResultadoAvaliacaoProjeto
    {
        public ResultadoAvaliacaoProjeto()
        {
            Notas = new List<double?>();
        }

        public string Aluno { get; set; }
        public string Grupo { get; set; }

        public double? MediaFinal { get; set; }

        public ICollection<double?> Notas { get; set; }
    }
}
