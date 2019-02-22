using AvaliadorPI.Domain.RootProjeto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class ProjetoConfiguration : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.ToTable(nameof(Projeto));

            builder
                .HasOne(x => x.Professor)
                .WithMany(x => x.Projetos)
                .HasForeignKey(x => x.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.AssociacaoAvaliadorProjeto)
                .WithOne(x => x.Projeto)
                .HasForeignKey(x => x.ProjetoId);
        }
    }
}
