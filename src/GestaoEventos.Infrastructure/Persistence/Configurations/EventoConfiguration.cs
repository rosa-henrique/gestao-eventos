using GestaoEventos.Domain.Eventos;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEventos.Infrastructure.Persistence.Configurations
{
    public class EventoConfiguration : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("eventos");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            builder.OwnsOne(e => e.Detalhes, detalhes =>
            {
                detalhes.Property(d => d.Nome)
                            .IsRequired()
                            .HasColumnName("nome")
                            .HasMaxLength(200);
                detalhes.Property(d => d.DataHora)
                            .IsRequired()
                            .HasColumnName("data_hora");
                detalhes.Property(d => d.Localizacao)
                            .IsRequired()
                            .HasColumnName("localizacao")
                            .HasMaxLength(500);
                detalhes.Property(d => d.CapacidadeMaxima)
                            .IsRequired()
                            .HasColumnName("capacidade_maxima");
                detalhes.Property(d => d.Status)
                            .IsRequired()
                            .HasColumnName("status");
            });
        }
    }
}