using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Aluno
{
    public class AvaliacaoAlunoViewModel
    {
        public AvaliacaoAlunoViewModel()
        {
            Criterios = new List<CriterioViewModel>();
            Avaliadores = new List<AvaliadorViewModel>();
        }

        public Guid AlunoId { get; internal set; }
        public Guid GrupoId { get; internal set; }

        public string NomeGrupo { get; set; }
        public string NomeProjeto { get; set; }
        public string Tema { get; set; }

        public IEnumerable<CriterioViewModel> Criterios { get; set; }
        public IEnumerable<AvaliadorViewModel> Avaliadores { get; set; }
    }
}
