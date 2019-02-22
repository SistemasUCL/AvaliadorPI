using AvaliadorPI.Domain.RootAvaliador;
using AvaliadorPI.Domain.RootCriterio;
using System;
using System.Collections.Generic;

namespace AvaliadorPI.Domain.RootAluno
{
    public class AvaliacaoAluno
    {
        public AvaliacaoAluno()
        {
            Criterios = new List<Criterio>();
            Avaliadores = new List<Avaliador>();
        }

        public Guid AlunoId { get; internal set; }
        public Guid GrupoId { get; internal set; }

        public string NomeGrupo { get; set; }
        public string NomeProjeto { get; set; }
        public string Tema { get; set; }

        public IEnumerable<Criterio> Criterios { get; set; }
        public IEnumerable<Avaliador> Avaliadores { get; set; }
    }
}
