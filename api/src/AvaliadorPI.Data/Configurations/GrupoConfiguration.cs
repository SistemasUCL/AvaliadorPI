using AvaliadorPI.Domain.RootGrupo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class GrupoConfiguration : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.ToTable(nameof(Grupo));

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(e => e.NomeProjeto)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(e => e.Nome)
                .HasColumnType("varchar(256)");

            builder
                .HasOne(x => x.Projeto)
                .WithMany(y => y.Grupos)
                .HasForeignKey(x => x.ProjetoId);
        }
    }
}
