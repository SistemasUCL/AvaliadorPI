using System;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public class AvaliacaoCalculavel
    {
        public Guid AvaliadorId { get; set; }

        public string Grupo { get; set; }
        public string Aluno { get; set; }
        public string Criterio { get; set; }

        public int Nota { get; set; }
        public int Peso { get; set; }
    }
}
