using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class EnqueteMap : IEntityTypeConfiguration<Enquete>
    {
        public void Configure(EntityTypeBuilder<Enquete> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Criado).HasColumnName("Criado").HasColumnType("datetime");
            builder.Property(x => x.Alterado).HasColumnName("Alterado").HasColumnType("datetime");
            builder.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SatisfacaoNivel).HasMaxLength(15).IsRequired();
        }
    }
}
