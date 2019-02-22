using AvaliadorPI.Domain.Associacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AssociacaoAlunoGrupoConfiguration : IEntityTypeConfiguration<AssociacaoAlunoGrupo>
    {
        public void Configure(EntityTypeBuilder<AssociacaoAlunoGrupo> builder)
        {
            builder.ToTable(nameof(AssociacaoAlunoGrupo));

            builder
                .HasKey(x => new { x.AlunoId, x.GrupoId });

            builder
                .HasOne(x => x.Aluno)
                .WithMany(b => b.AssociacaoAlunoGrupo)
                .HasForeignKey(x => x.AlunoId)
                .IsRequired();

            builder
                .HasOne(x => x.Grupo)
                .WithMany(x => x.AssociacaoAlunoGrupo)
                .HasForeignKey(x => x.GrupoId)
                .IsRequired();

        }
    }
}
