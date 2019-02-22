using System;

namespace AvaliadorPI.API.ViewModels.Administrador
{
    public class AdministradorViewModel
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
