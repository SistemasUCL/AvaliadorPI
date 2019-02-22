using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootGrupo;
using System;

namespace AvaliadorPI.Domain.Associacoes
{
    public class AssociacaoAlunoGrupo
    {
        public Guid AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public Guid GrupoId { get; set; }
        public Grupo Grupo { get; set; }
    }
}
