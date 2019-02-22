using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.API.ViewModels.Professor
{
    public class ProfessorFormViewModel
    {
        public ProfessorFormViewModel()
        {
            Disciplinas = Enumerable.Empty<Guid>();
        }

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Matricula { get; set; }
        public IEnumerable<Guid> Disciplinas { get; set; }
    }
}
