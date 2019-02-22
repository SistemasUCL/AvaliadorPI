using AvaliadorPI.Domain.RootAvaliador;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AvaliadorConfiguration : IEntityTypeConfiguration<Avaliador>
    {
        public void Configure(EntityTypeBuilder<Avaliador> builder)
        {
            builder.ToTable(nameof(Avaliador));

            builder
                .HasOne(x => x.Usuario)
                .WithOne()
                .HasForeignKey<Avaliador>(a => a.Id);

            builder
                .HasMany(x => x.AssociacaoAvaliadorProjeto)
                .WithOne(x => x.Avaliador)
                .HasForeignKey(x => x.AvaliadorId);
        }
    }
}
