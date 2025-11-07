using ErrorOr;

using EventFlow.Eventos.Application.Response;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Buscar;

public record BuscarRequest : IRequest<ErrorOr<IEnumerable<EventoResponse>>>;