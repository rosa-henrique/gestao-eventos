using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Compras;

public class IngressoComprado : Entity
{
    public Guid IngressoId { get; private set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }

    public IngressoComprado(Guid ingressoId, int quantidade, decimal precoUnitario, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        IngressoId = ingressoId;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }

    public decimal ValorTotal => PrecoUnitario * Quantidade;

    private IngressoComprado() { }
}