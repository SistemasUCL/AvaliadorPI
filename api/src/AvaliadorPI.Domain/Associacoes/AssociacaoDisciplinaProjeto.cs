using AvaliadorPI.Domain.RootDisciplina;
using AvaliadorPI.Domain.RootProjeto;
using System;

namespace AvaliadorPI.Domain.Associacoes
{
    public class AssociacaoDisciplinaProjeto
    {
        public Guid DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public Guid ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
    }
}
