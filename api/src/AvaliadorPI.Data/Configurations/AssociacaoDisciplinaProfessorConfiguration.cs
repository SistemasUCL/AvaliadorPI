using AvaliadorPI.Domain.Associacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AssociacaoDisciplinaProfessorConfiguration : IEntityTypeConfiguration<AssociacaoDisciplinaProfessor>
    {
        public void Configure(EntityTypeBuilder<AssociacaoDisciplinaProfessor> builder)
        {
            builder.ToTable(nameof(AssociacaoDisciplinaProfessor));

            builder
                .HasKey(x => new { x.DisciplinaId, x.ProfessorId });

            builder
                .HasOne(x => x.Disciplina)
                .WithMany(b => b.AssociacaoDisciplinaProfessor)
                .HasForeignKey(x => x.DisciplinaId)
                .IsRequired();

            builder
                .HasOne(x => x.Professor)
                .WithMany(x => x.AssociacaoDisciplinaProfessor)
                .HasForeignKey(x => x.ProfessorId)
                .IsRequired();

        }
    }
}
