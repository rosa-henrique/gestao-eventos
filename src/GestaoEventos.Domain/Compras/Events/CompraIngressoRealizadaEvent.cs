using GestaoEventos.Domain.Common;
using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Compras.Events;

public record CompraIngressoRealizadaEvent(Evento Evento, Guid UsuarioId) : IDomainEvent;