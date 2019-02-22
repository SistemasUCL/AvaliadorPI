using System;

namespace AvaliadorPI.API.ViewModels.Aluno
{
    public class AvaliadorViewModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
    }
}
