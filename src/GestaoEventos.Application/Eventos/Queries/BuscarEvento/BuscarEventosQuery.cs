using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEvento;

public record BuscarEventosQuery(Guid Id) : IRequest<ErrorOr<Evento>>
{
}