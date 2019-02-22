using AvaliadorPI.Domain.RootAluno;
using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootGrupo;
using System;

namespace AvaliadorPI.Domain.RootAvaliacao
{
    public class Avaliacao : Entity<Avaliacao>
    {
        public enum EnumTipo : byte { Grupo, Aluno }

        public Avaliacao()
        {
            Tipo = EnumTipo.Grupo;
            Data = DateTime.Now;
        }

        public DateTime Data { get; set; }
        public int Nota { get; set; }
        public EnumTipo Tipo { get; set; }

        public Guid? GrupoId { get; set; }
        public virtual Grupo Grupo { get; set; }

        public Guid? AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }

        public Guid AvaliadorId { get; set; }
        public virtual Avaliador Avaliador { get; set; }

        public Guid CriterioId { get; set; }
        public virtual Criterio Criterio { get; set; }
    }
}
