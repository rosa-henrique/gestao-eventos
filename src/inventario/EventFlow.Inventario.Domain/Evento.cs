using EventFlow.Shared.Domain;

namespace EventFlow.Inventario.Domain;

public class Evento : Entity
{
    public int CapacidadeMaxima { get; private set; }
    public StatusEvento Status { get; private set; } = null!;
    public Guid CriadoPor { get; private set; }

    public virtual ICollection<Ingresso> Ingressos { get; private set; } = null!;

    public Evento(Guid id, int capacidadeMaxima, StatusEvento status, Guid criadoPor)
        : base(id)
    {
        CapacidadeMaxima = capacidadeMaxima;
        Status = status;
        CriadoPor = criadoPor;
    }

    public void Alterar(int capacidadeMaxima, StatusEvento status)
    {
        CapacidadeMaxima = capacidadeMaxima;
        Status = status;
    }

    private Evento() { }
}