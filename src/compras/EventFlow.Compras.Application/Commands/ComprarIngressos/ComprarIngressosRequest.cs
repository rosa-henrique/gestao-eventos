using ErrorOr;

using MediatR;

namespace EventFlow.Compras.Application.Commands.ComprarIngressos;

public record ComprarIngressosRequest(IEnumerable<(Guid IngressoId, int Quantidade)> IngressosCompra) : IRequest<ErrorOr<Success>>;