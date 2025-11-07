using EventFlow.Eventos.Application.Response;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.BuscarPorId;

public record BuscarPorIdRequest(Guid Id) : IRequest<ErrorOr.ErrorOr<EventoResponse>>;