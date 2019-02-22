using AvaliadorPI.Domain.RootProfessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaliadorPI.Data.Configurations
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable(nameof(Professor));

            builder
                .Property(e => e.Matricula)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder
                .HasOne(x => x.Usuario)
                .WithOne()
                .HasForeignKey<Professor>(a => a.Id);
        }
    }
}
