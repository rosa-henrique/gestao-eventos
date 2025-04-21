using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarIngressoPorEvento;

public class BuscarIngressoPorEventoQueryHandler(IEventoRepository eventoRepository)
    : IRequestHandler<BuscarIngressoPorEventoQuery,
        ErrorOr<IEnumerable<IngressoResponse>>>
{
    public async Task<ErrorOr<IEnumerable<IngressoResponse>>> Handle(BuscarIngressoPorEventoQuery request, CancellationToken cancellationToken)
    {
        var evento = await eventoRepository.BuscarPorId(request.EventoId, cancellationToken);

        return evento is not null
            ? evento.Ingressos.Select(i => (IngressoResponse)i).ToList()
            : Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
    }
}