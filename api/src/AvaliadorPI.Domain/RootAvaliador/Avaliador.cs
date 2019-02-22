using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootAvaliacao;
using AvaliadorPI.Domain.RootProjeto;
using AvaliadorPI.Domain.RootUsuario;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.Domain.RootAvaliador
{
    public class Avaliador : Entity<Avaliador>
    {
        public Avaliador()
        {
            Avaliacoes = new List<Avaliacao>();
        }

        public Usuario Usuario { get; set; }

        public virtual ICollection<Avaliacao> Avaliacoes { get; set; }

        public virtual ICollection<AssociacaoAvaliadorProjeto> AssociacaoAvaliadorProjeto { get; set; }
    }
}
