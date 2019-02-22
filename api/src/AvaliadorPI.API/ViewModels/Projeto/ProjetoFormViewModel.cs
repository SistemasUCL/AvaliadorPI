using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaliadorPI.API.ViewModels.Projeto
{
    public class ProjetoFormViewModel
    {
        public enum EnumEstado { Elaboracao, Avaliacao, Encerrado }

        public ProjetoFormViewModel()
        {
            Disciplinas = Enumerable.Empty<Guid>();
        }

        public string Periodo { get; set; }
        public string Tema { get; set; }
        public string Descricao { get; set; }
        public EnumEstado Estado { get; set; }

        public Guid ProfessorId { get; set; }

        public IEnumerable<Guid> Disciplinas { get; set; }
    }
}
