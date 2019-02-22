using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootProjeto;
using System;

namespace AvaliadorPI.Domain.Associacoes
{
    public class AssociacaoAvaliadorProjeto
    {
        public Guid AvaliadorId { get; set; }
        public virtual Avaliador Avaliador { get; set; }

        public Guid ProjetoId { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
