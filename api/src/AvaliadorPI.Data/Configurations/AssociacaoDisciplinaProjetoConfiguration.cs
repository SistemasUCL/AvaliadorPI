using AvaliadorPI.Domain.Associacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AssociacaoDisciplinaProjetoConfiguration : IEntityTypeConfiguration<AssociacaoDisciplinaProjeto>
    {
        public void Configure(EntityTypeBuilder<AssociacaoDisciplinaProjeto> builder)
        {
            builder.ToTable(nameof(AssociacaoDisciplinaProjeto));

            builder
                .HasKey(x => new { x.DisciplinaId, x.ProjetoId });

            builder
                .HasOne(x => x.Disciplina)
                .WithMany(b => b.AssociacaoDisciplinaProjeto)
                .HasForeignKey(x => x.DisciplinaId)
                .IsRequired();

            builder
                .HasOne(x => x.Projeto)
                .WithMany(x => x.AssociacaoDisciplinaProjeto)
                .HasForeignKey(x => x.ProjetoId)
                .IsRequired();
        }
    }
}
