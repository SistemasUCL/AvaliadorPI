using System;
using System.Collections.Generic;

namespace AvaliadorPI.API.ViewModels.Grupo
{
    public class GrupoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeProjeto { get; set; }

        public Guid ProjetoId { get; set; }
        public ProjetoViewModel Projeto { get; set; }

        public IEnumerable<AlunoViewModel> Integrantes { get; set; }
    }
}
