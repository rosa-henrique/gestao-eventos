using EventFlow.Shared.Domain;

namespace EventFlow.Compras.Domain;

public class CompraIngresso : Entity, IAggregateRoot
{
    public Guid UsuarioId { get; private set; }
    public DateTime DataHoraCompra { get; private set; }
    public decimal ValorTotal { get; private set; }
    public StatusCompra Status { get; private set; } = null!;

    private readonly List<ItemCompra> _itens = new();
    public IReadOnlyList<ItemCompra> Itens => _itens.AsReadOnly();
}