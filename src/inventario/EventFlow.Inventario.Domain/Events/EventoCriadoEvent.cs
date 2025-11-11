using MediatR;

namespace EventFlow.Inventario.Domain.Events;

public record EventoCriadoEvent(Guid Id, int CapacidadeMaxima, StatusEvento Status, Guid CriadoPor) : INotification;