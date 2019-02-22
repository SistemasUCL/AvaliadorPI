using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Disciplina
{
    public class DisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public IEnumerable<ProfessorViewModel> Professores { get; set; }
        public IEnumerable<ProjetoViewModel> Projetos { get; set; }
    }
}