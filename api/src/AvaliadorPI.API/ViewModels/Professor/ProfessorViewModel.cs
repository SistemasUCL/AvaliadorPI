using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.API.ViewModels.Professor
{
    public class ProfessorViewModel
    {
        public ProfessorViewModel()
        {
            Projetos = Enumerable.Empty<ProjetoViewModel>();
            Disciplinas = Enumerable.Empty<Guid>();
        }

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public string Matricula { get; set; }

        public IEnumerable<ProjetoViewModel> Projetos { get; set; }
        public IEnumerable<Guid> Disciplinas { get; set; }
    }
}
