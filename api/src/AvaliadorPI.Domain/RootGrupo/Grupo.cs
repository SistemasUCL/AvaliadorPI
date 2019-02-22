using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootProjeto;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Domain.RootGrupo
{
    public class Grupo : Entity<Grupo>
    {
        public Grupo()
        {
            AssociacaoAlunoGrupo = new List<AssociacaoAlunoGrupo>();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeProjeto { get; set; }
        public byte[] QRCode { get; set; }

        public Guid ProjetoId { get; set; }
        public virtual Projeto Projeto { get; set; }

        public virtual ICollection<AssociacaoAlunoGrupo> AssociacaoAlunoGrupo { get; set; }
    }
}
