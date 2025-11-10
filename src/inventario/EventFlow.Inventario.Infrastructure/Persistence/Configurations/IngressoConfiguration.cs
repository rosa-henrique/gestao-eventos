using EventFlow.Inventario.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Inventario.Infrastructure.Persistence.Configurations;

public class IngressoConfiguration : IEntityTypeConfiguration<Ingresso>
{
    public void Configure(EntityTypeBuilder<Ingresso> builder)
    {
        builder.ToTable("ingressos");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(d => d.Nome)
            .IsRequired()
            .HasColumnName("nome")
            .HasMaxLength(200);

        builder.Property(d => d.Descricao)
            .IsRequired()
            .HasColumnName("descricao")
            .HasMaxLength(1000);

        builder.Property(i => i.Preco)
            .IsRequired()
            .HasPrecision(8, 2)
            .HasColumnName("preco");

        builder.Property(i => i.QuantidadeTotal)
            .IsRequired()
            .HasColumnName("quantidade");

        builder.Property(i => i.EventoId)
            .IsRequired()
            .HasColumnName("evento_id");

        builder.HasOne(i => i.Evento)
            .WithMany(i => i.Ingressos)
            .HasForeignKey(i => i.EventoId);
    }
}