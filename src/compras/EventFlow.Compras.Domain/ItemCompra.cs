namespace EventFlow.Compras.Domain;

public record ItemCompra
{
    public Guid IngressoId { get; init; }
    public decimal PrecoUnitario { get; init; }
    public int Quantidade { get; init; }
}