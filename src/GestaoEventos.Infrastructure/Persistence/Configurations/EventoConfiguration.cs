using GestaoEventos.Domain.Eventos;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEventos.Infrastructure.Persistence.Configurations;

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
                        .HasColumnName("status")
                        .HasConversion(
                        s => s.Value,
                        s => StatusEvento.FromValue(s));
        });

        builder.OwnsMany(e => e.Ingressos, ingressos =>
        {
            ingressos.ToTable("eventos_ingressos");

            ingressos.HasKey(e => e.Id);
            ingressos.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedNever();

            ingressos.OwnsOne(i => i.Tipo, tipoIngresso =>
            {
                tipoIngresso.Property(d => d.Nome)
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasMaxLength(200);

                tipoIngresso.Property(d => d.Descricao)
                            .IsRequired()
                            .HasColumnName("descricao")
                            .HasMaxLength(100);
            });

            ingressos.Property(i => i.Preco)
                    .IsRequired()
                    .HasPrecision(8, 2)
                    .HasColumnName("preco");

            ingressos.Property(i => i.Quantidade)
                    .IsRequired()
                    .HasColumnName("quantidade");
        });
    }
}