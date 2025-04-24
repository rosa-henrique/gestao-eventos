using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Compras.Commands.ComprarIngressos;

public record ComprarIngressosCommand(Guid SessaoId, IEnumerable<IngressosCompra> IngressosCompra) : IRequest<ErrorOr<Success>>;

public record IngressosCompra(Guid IngressoId, int Quantidade);