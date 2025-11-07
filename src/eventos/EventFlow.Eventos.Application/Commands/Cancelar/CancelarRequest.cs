using ErrorOr;

using EventFlow.Eventos.Application.Response;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Cancelar;

public record CancelarRequest(Guid Id) : IRequest<ErrorOr<EventoResponse>>;