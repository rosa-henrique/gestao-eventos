using EventFlow.Shared.Domain;

namespace EventFlow.Eventos.Domain.Events;

public record EventoCriadoEvent(Evento Evento) : IDomainEvent;