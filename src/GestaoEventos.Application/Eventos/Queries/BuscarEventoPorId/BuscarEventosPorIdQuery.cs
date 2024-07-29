using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventoPorId;

public record BuscarEventosPorIdQuery(Guid Id) : IRequest<ErrorOr<Evento>>
{
}