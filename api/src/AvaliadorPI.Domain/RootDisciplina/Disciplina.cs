using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootProfessor;
using AvaliadorPI.Domain.RootProjeto;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.Domain.RootDisciplina
{
    public class Disciplina : Entity<Disciplina>
    {
        public Disciplina()
        {
            AssociacaoDisciplinaProfessor = new List<AssociacaoDisciplinaProfessor>();
            AssociacaoDisciplinaProjeto = new List<AssociacaoDisciplinaProjeto>();
        }

        public string Nome { get; set; }

        public virtual ICollection<AssociacaoDisciplinaProfessor> AssociacaoDisciplinaProfessor { get; set; }

        public virtual ICollection<AssociacaoDisciplinaProjeto> AssociacaoDisciplinaProjeto { get; set; }
    }
}
