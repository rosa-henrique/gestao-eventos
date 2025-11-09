using EventFlow.Inventario.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Inventario.Infrastructure.Persistence.Configurations;

public class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable("eventos");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(d => d.Status)
            .IsRequired()
            .HasColumnName("status")
            .HasConversion(
                s => s.Value,
                s => StatusEvento.FromValue(s));

        builder.Property(d => d.CriadoPor)
            .IsRequired()
            .HasColumnName("criado_por");
    }
}