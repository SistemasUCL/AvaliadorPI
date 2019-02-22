using AvaliadorPI.Data.Context;
using AvaliadorPI.Domain.RootUsuario;

namespace AvaliadorPI.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AvaliadorPIContext context) : base(context) { }
    }
}
