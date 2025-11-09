using EventFlow.Shared.Domain;

namespace EventFlow.Inventario.Domain;

public class Evento : Entity
{
    public StatusEvento Status { get; private set; } = null!;
    public Guid CriadoPor { get; private set; }

    public Evento(Guid id, StatusEvento status, Guid criadoPor)
        : base(id)
    {
        Status = status;
        CriadoPor = criadoPor;
    }

    public void Alterar(StatusEvento status, Guid criadoPor)
    {
        Status = status;
        CriadoPor = criadoPor;
    }

    private Evento() { }
}