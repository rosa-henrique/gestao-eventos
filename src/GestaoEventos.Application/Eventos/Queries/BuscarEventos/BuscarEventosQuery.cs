using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventos;

public record BuscarEventosQuery : IRequest<ErrorOr<IEnumerable<BuscarEventosResponse>>>
{
}