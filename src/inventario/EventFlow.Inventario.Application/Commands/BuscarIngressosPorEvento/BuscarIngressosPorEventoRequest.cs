using ErrorOr;

using EventFlow.Inventario.Application.Response;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.BuscarIngressosPorEvento;

public record BuscarIngressosPorEventoRequest(Guid EventoId) : IRequest<ErrorOr<IList<IngressoResponse>>>;