using AvaliadorPI.Domain.RootUsuario;

namespace AvaliadorPI.Domain.RootAdministrador
{
    public class Administrador : Entity<Administrador>
    {
        public virtual Usuario Usuario { get; set; }
    }
}
