using AvaliadorPI.Domain.RootUsuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));

            builder
                .Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(e => e.SobreNome)
                .HasColumnType("varchar(50)");

            builder
                .Property(e => e.Telefone)
                .HasColumnType("varchar(20)");
        }
    }
}
