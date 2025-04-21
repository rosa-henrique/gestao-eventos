using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarIngressoPorEvento;

public record BuscarIngressoPorEventoQuery(Guid EventoId) : IRequest<ErrorOr<IEnumerable<IngressoResponse>>>;