using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Projeto
{
    public class ProjetoViewModel
    {
        public enum EnumEstado { Elaboracao, Avaliacao, Encerrado }

        public Guid Id { get; set; }

        public string Periodo { get; set; }
        public string Tema { get; set; }
        public string Descricao { get; set; }
        public EnumEstado Estado { get; set; }

        public Guid ProfessorId { get; set; }
        public IEnumerable<Guid> Disciplinas { get; set; }
    }
}
