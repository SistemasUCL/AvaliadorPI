using AvaliadorPI.Domain.RootProjeto;
using System;

namespace AvaliadorPI.Domain.RootCriterio
{
    public class Criterio : Entity<Criterio>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Peso { get; set; }
        public int Ordem { get; set; }

        public Guid ProjetoId { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
