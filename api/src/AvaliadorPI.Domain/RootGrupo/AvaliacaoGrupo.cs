using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootCriterio;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Domain.RootGrupo
{
    public class AvaliacaoGrupo
    {
        public AvaliacaoGrupo()
        {
            Criterios = new List<Criterio>();
            Alunos = new List<Aluno>();
        }
        public Guid GrupoId { get; set; }
        public string Nome { get; set; }
        public string Projeto { get; set; }
        public string Tema { get; set; }
        public IEnumerable<Criterio> Criterios { get; set; }
        public IEnumerable<Aluno> Alunos { get; set; }
    }
}
