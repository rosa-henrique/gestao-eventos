using MediatR;

namespace EventFlow.Inventario.Domain.Events;

public record EventoCriadoEvent(Guid Id, StatusEvento Status, Guid CriadoPor) : INotification;