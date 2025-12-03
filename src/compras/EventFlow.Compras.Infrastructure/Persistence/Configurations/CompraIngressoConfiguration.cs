using EventFlow.Compras.Domain;
using EventFlow.Shared.Application.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlow.Compras.Infrastructure.Persistence.Configurations;

public class CompraIngressoConfiguration : IEntityTypeConfiguration<CompraIngresso>
{
    public void Configure(EntityTypeBuilder<CompraIngresso> builder)
    {
        builder.ToTable("compra_ingresos");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(d => d.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id");

        builder.Property(d => d.DataHoraCompra)
            .IsRequired()
            .HasColumnName("data_hora_compra");

        builder.Property(d => d.ValorTotal)
            .IsRequired()
            .HasColumnName("valor_total");

        builder.Property(d => d.Status)
            .IsRequired()
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasConversion(
                s => s.Name,
                s => StatusCompra.FromName(s));

        builder.OwnsMany(c => c.Itens, itemBuilder =>
        {
            itemBuilder.ToTable("compra_ingresos_itens");

            itemBuilder.HasKey("Id");
            itemBuilder.Property<Guid>("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            itemBuilder.WithOwner().HasForeignKey("compra_ingressos_id");

            itemBuilder.Property(d => d.IngressoId)
                .IsRequired()
                .HasColumnName("ingresso_id");

            itemBuilder.Property(d => d.PrecoUnitario)
                .IsRequired()
                .HasColumnName("preco_unitario");

            itemBuilder.Property(d => d.Quantidade)
                .IsRequired()
                .HasColumnName("quantidade");
        });
    }
}