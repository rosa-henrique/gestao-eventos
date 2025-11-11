using MediatR;

namespace EventFlow.Inventario.Domain.Events;

public record EventoAlteradoEvent(Guid Id, int CapacidadeMaxima, StatusEvento Status) : INotification;