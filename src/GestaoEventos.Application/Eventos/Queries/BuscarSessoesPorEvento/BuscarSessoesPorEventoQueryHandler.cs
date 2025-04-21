using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarSessoesPorEvento;

public class BuscarSessoesPorEventoQueryHandler(IEventoRepository eventoRepository)
    : IRequestHandler<BuscarSessoesPorEventoQuery,
        ErrorOr<IEnumerable<SessaoResponse>>>
{
    public async Task<ErrorOr<IEnumerable<SessaoResponse>>> Handle(BuscarSessoesPorEventoQuery request, CancellationToken cancellationToken)
    {
        var evento = await eventoRepository.BuscarPorId(request.EventoId, cancellationToken);

        return evento is not null
            ? evento.Sessoes.Select(i => (SessaoResponse)i).ToList()
            : Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
    }
}