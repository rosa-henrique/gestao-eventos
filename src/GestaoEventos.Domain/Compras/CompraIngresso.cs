using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Compras;

public class CompraIngresso : Entity, IAggregateRoot
{
    public Guid UsuarioId { get; private set; }
    public Guid SessaoId { get; private set; }
    public DateTime DataCompra { get; private set; }
    public decimal ValorTotal { get; private set; }

    private readonly List<IngressoComprado> _ingressos = [];
    public IReadOnlyCollection<IngressoComprado> Ingressos => [.. _ingressos];

    public CompraIngresso(Guid usuarioId, Guid sessaoId, IEnumerable<IngressoComprado> ingressos, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        UsuarioId = usuarioId;
        SessaoId = sessaoId;
        DataCompra = DateTime.UtcNow;

        _ingressos.AddRange(ingressos);
        ValorTotal = _ingressos.Sum(i => i.ValorTotal);
    }

    private CompraIngresso() { }
}