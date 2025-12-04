using ErrorOr;

using MediatR;

namespace EventFlow.Compras.Application.Commands.ComprarIngressos;

public record ComprarIngressosRequest(Dictionary<Guid, int> IngressosCompra) : IRequest<ErrorOr<Success>>;