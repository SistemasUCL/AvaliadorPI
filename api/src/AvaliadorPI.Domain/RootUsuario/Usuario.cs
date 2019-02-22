namespace AvaliadorPI.Domain.RootUsuario
{
    public class Usuario : Entity<Usuario>
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public string NomeCompleto => Nome + " " + SobreNome;
    }
}
