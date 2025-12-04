using ErrorOr;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.ProcessarItens;

public record ProcessarItensRequest(IDictionary<Guid, int> IngressosCompra) : IRequest<ErrorOr<IEnumerable<ProcessarItensRequestResponse>>>;