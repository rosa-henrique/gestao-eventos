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

        builder.OwnsMany(e => e.Ingressos, ingressos =>
        {
            ingressos.ToTable("eventos_ingressos");

            ingressos.WithOwner().HasForeignKey("evento_id");

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
        }).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Ingressos).Metadata.SetField("_ingressos");

        builder.OwnsMany(e => e.Sessoes, sessoes =>
        {
            sessoes.ToTable("eventos_sessoes");

            sessoes.WithOwner().HasForeignKey("evento_id");

            sessoes.HasKey(e => e.Id);
            sessoes.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedNever();

            sessoes.Property(d => d.Nome)
                .IsRequired()
                .HasColumnName("nome")
                .HasMaxLength(200);

            sessoes.Property(d => d.DataHoraInicio)
                .IsRequired()
                .HasColumnName("data_hora_inico");

            sessoes.Property(d => d.DataHoraFim)
                .IsRequired()
                .HasColumnName("data_hora_fim");
        }).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Sessoes).Metadata.SetField("_sessoes");
    }
}