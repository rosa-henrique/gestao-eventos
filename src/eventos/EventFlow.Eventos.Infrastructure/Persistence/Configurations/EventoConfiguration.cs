using EventFlow.Eventos.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Eventos.Infrastructure.Persistence.Configurations;

public class EventoConfiguration : IEntityTypeConfiguration<Evento>
{
    public void Configure(EntityTypeBuilder<Evento> builder)
    {
        builder.ToTable("eventos");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(d => d.Nome)
            .IsRequired()
            .HasColumnName("nome")
            .HasMaxLength(200);

        builder.Property(d => d.DataHoraInicio)
            .IsRequired()
            .HasColumnName("data_hora_inico");

        builder.Property(d => d.DataHoraFim)
            .IsRequired()
            .HasColumnName("data_hora_fim");

        builder.Property(d => d.Localizacao)
            .IsRequired()
            .HasColumnName("localizacao")
            .HasMaxLength(500);

        builder.Property(d => d.CapacidadeMaxima)
            .IsRequired()
            .HasColumnName("capacidade_maxima");

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