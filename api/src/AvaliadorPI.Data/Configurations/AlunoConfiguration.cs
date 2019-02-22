using AvaliadorPI.Domain.RootAluno;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable(nameof(Aluno));
            
            builder
                .Property(e => e.Matricula)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder
                .HasOne(x => x.Usuario)
                .WithOne()
                .HasForeignKey<Aluno>(a => a.Id);
        }
    }
}
