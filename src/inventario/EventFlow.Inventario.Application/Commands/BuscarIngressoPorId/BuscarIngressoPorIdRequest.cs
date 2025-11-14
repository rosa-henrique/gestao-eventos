using ErrorOr;

using EventFlow.Inventario.Application.Response;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.BuscarIngressoPorId;

public record BuscarIngressoPorIdRequest(Guid Id) : IRequest<ErrorOr<IngressoResponse>>;