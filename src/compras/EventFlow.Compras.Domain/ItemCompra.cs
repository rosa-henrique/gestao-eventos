namespace EventFlow.Compras.Domain;

public record ItemCompra
{
    public required Guid IngressoId { get; init; }
    public required Guid EventoId { get; init; }
    public required decimal PrecoUnitario { get; init; }
    public required int Quantidade { get; init; }
}