using MediatR;

namespace EventFlow.Inventario.Domain.Events;

public record EventoAlteradoEvent(Guid Id, StatusEvento Status) : INotification;