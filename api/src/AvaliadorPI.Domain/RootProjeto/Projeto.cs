using AvaliadorPI.Domain.Associacoes;
using AvaliadorPI.Domain.RootCriterio;
using AvaliadorPI.Domain.RootGrupo;
using AvaliadorPI.Domain.RootProfessor;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Domain.RootProjeto
{
    public class Projeto : Entity<Projeto>
    {
        public enum EnumEstado { Elaboracao, Avaliacao, Encerrado }

        public Projeto()
        {
            Criterios = new List<Criterio>();
            Grupos = new List<Grupo>();
            AssociacaoDisciplinaProjeto = new List<AssociacaoDisciplinaProjeto>();
            AssociacaoAvaliadorProjeto = new List<AssociacaoAvaliadorProjeto>();
        }

        public string Periodo { get; set; }
        public string Tema { get; set; }
        public string Descricao { get; set; }
        public EnumEstado Estado { get; set; }

        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<Criterio> Criterios { get; set; }

        public virtual ICollection<AssociacaoDisciplinaProjeto> AssociacaoDisciplinaProjeto { get; set; }

        public virtual ICollection<AssociacaoAvaliadorProjeto> AssociacaoAvaliadorProjeto { get; set; }
    }
}
