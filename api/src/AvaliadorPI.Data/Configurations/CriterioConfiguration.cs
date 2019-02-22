using AvaliadorPI.Domain.RootCriterio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class CriterioConfiguration : IEntityTypeConfiguration<Criterio>
    {
        public void Configure(EntityTypeBuilder<Criterio> builder)
        {
            builder.ToTable(nameof(Criterio));

            builder
                .Property(e => e.Titulo)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(e => e.Descricao)
                .HasColumnType("varchar(256)")
                .IsRequired();
        }
    }
}
