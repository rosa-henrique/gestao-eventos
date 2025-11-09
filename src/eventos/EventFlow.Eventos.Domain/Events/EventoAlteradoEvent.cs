using EventFlow.Shared.Domain;

namespace EventFlow.Eventos.Domain.Events;

public record EventoAlteradoEvent(Evento Evento) : IDomainEvent;