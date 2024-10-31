using GestaoEventos.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEventos.Infrastructure.Persistence.Configurations;

public class UsuarioRepository : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(d => d.Nome)
            .IsRequired()
            .HasColumnName("nome")
            .HasMaxLength(100);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasMaxLength(100);

        builder.Property(d => d.Permissao)
            .IsRequired()
            .HasColumnName("permissao")
            .HasConversion(
                s => s.Value,
                s => Permissao.FromValue(s));
    }
}