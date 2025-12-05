using EventFlow.Shared.Domain;

namespace EventFlow.Compras.Domain;

public class CompraIngresso : Entity, IAggregateRoot
{
    public Guid UsuarioId { get; private set; }
    public DateTime DataHoraCompra { get; private set; }
    public decimal ValorTotal { get; private set; }
    public StatusCompra Status { get; private set; } = null!;

    private readonly IList<ItemCompra> _itens = [];
    public IReadOnlyCollection<ItemCompra> Itens => [.. _itens];

    private CompraIngresso(Guid usuarioId, IList<ItemCompra> items)
    {
        _itens = items;
        UsuarioId = usuarioId;
        DataHoraCompra = DateTime.UtcNow;
        Status = StatusCompra.Pendente;
        ValorTotal = _itens.Sum(i => i.Quantidade * i.PrecoUnitario);
    }

    public static CompraIngresso Criar(Guid usuarioId, IList<ItemCompra> items)
    {
        return new CompraIngresso(usuarioId, items);
    }

    protected CompraIngresso() { }
}