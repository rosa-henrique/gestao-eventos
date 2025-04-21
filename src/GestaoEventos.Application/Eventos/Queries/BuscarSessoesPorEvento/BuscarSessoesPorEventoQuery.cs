using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarSessoesPorEvento;

public record BuscarSessoesPorEventoQuery(Guid EventoId) : IRequest<ErrorOr<IEnumerable<SessaoResponse>>>;