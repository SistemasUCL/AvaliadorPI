using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootProfessor;
using System;

namespace AvaliadorPI.Domain.Associacoes
{
    public class AssociacaoDisciplinaProfessor
    {
        public Guid DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}
