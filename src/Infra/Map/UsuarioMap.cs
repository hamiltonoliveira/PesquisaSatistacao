using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Criado).HasColumnName("Criado").HasColumnType("datetime");
            builder.Property(x => x.Alterado).HasColumnName("Alterado").HasColumnType("datetime");
            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(200).IsRequired();
        }
    }
}
