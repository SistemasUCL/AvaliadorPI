using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootUsuario;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.Domain.RootAluno
{
    public class Aluno : Entity<Aluno>
    {
        public Aluno()
        {
            AssociacaoAlunoGrupo = new List<AssociacaoAlunoGrupo>();
        }

        public string Matricula { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<AssociacaoAlunoGrupo> AssociacaoAlunoGrupo { get; set; }
    }
}
