using AvaliadorPI.Domain.RootAdministrador;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AdministradorConfiguration : IEntityTypeConfiguration<Administrador>
    {
        public void Configure(EntityTypeBuilder<Administrador> builder)
        {
            builder.ToTable(nameof(Administrador));

            builder
                .HasOne(x => x.Usuario)
                .WithOne()
                .HasForeignKey<Administrador>(a => a.Id);
        }
    }
}
