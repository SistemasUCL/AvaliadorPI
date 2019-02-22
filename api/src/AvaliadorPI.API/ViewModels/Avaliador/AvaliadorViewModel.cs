using System;

namespace AvaliadorPI.API.ViewModels.Avaliador
{
    public class AvaliadorViewModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
