using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Grupo
{
    public class AvaliacaoGrupoViewModel
    {
        public AvaliacaoGrupoViewModel()
        {
            Criterios = new List<CriterioViewModel>();
            Alunos = new List<AlunoViewModel>();
        }

        public Guid GrupoId { get; set; }
        public string Nome { get; set; }
        public string Tema { get; set; }
        public string Projeto { get; set; }
        public IEnumerable<CriterioViewModel> Criterios { get; set; }
        public IEnumerable<AlunoViewModel> Alunos { get; set; }
    }
}
