using System;

namespace AvaliadorPI.API.ViewModels.Shared
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
