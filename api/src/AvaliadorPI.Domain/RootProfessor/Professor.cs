using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootProjeto;
using AvaliadorPI.Domain.RootUsuario;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.Domain.RootProfessor
{
    public class Professor : Entity<Professor>
    {
        public string Matricula { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Projeto> Projetos { get; set; }

        public virtual ICollection<AssociacaoDisciplinaProfessor> AssociacaoDisciplinaProfessor { get; set; }
    }
}
