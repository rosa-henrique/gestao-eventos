using GestaoEventos.Domain.Compras;
using GestaoEventos.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoEventos.Infrastructure.Persistence.Configurations;

public class CompraIngressoConfiguration : IEntityTypeConfiguration<CompraIngresso>
{
    public void Configure(EntityTypeBuilder<CompraIngresso> builder)
    {
        builder.ToTable("compra_ingresso");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(i => i.UsuarioId)
            .IsRequired()
            .HasColumnName("usuario_id");

        builder.Property(i => i.SessaoId)
            .IsRequired()
            .HasColumnName("sessao_id");

        builder.Property(i => i.DataCompra)
            .IsRequired()
            .HasColumnName("data_compra");

        builder.Property(i => i.ValorTotal)
            .IsRequired()
            .HasPrecision(8, 2)
            .HasColumnName("valor_total");

        builder.OwnsMany(c => c.Ingressos, ingresso =>
        {
            ingresso.ToTable("compra_ingresso_ingressos");

            ingresso.WithOwner().HasForeignKey("compra_ingresso_id");

            ingresso.HasKey(e => e.Id);
            ingresso.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedNever();

            ingresso.Property(i => i.PrecoUnitario)
                .IsRequired()
                .HasPrecision(8, 2)
                .HasColumnName("preco_unitario");

            ingresso.Property(i => i.Quantidade)
                .IsRequired()
                .HasColumnName("quantidade");

            ingresso.Property(i => i.IngressoId)
                .IsRequired()
                .HasColumnName("ingresso_id");

            ingresso.Ignore(a => a.ValorTotal);
        }).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Ingressos).Metadata.SetField("_ingressos");
    }
}