using System;

namespace AvaliadorPI.API.ViewModels.Grupo
{
    public class GrupoFormViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string NomeProjeto { get; set; }
        public Guid ProjetoId { get; set; }
    }
}
