using AvaliadorPI.Domain.Associacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AssociacaoAvaliadorProjetoConfiguration : IEntityTypeConfiguration<AssociacaoAvaliadorProjeto>
    {
        public void Configure(EntityTypeBuilder<AssociacaoAvaliadorProjeto> builder)
        {
            builder.ToTable(nameof(AssociacaoAvaliadorProjeto));

            builder
                .HasKey(x => new { x.AvaliadorId, x.ProjetoId });

            builder
                .HasOne(x => x.Avaliador)
                .WithMany(b => b.AssociacaoAvaliadorProjeto)
                .HasForeignKey(x => x.AvaliadorId)
                .IsRequired();

            builder
                .HasOne(x => x.Projeto)
                .WithMany(x => x.AssociacaoAvaliadorProjeto)
                .HasForeignKey(x => x.ProjetoId)
                .IsRequired();
        }
    }
}
