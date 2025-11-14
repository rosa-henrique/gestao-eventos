using ErrorOr;

using EventFlow.Inventario.Application.Response;
using EventFlow.Inventario.Domain;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.BuscarIngressosPorEvento;

public class BuscarIngressosPorEventoRequestHandler(IIngressoRepository ingressoRepository, IEventoRepository eventoRepository) : IRequestHandler<BuscarIngressosPorEventoRequest, ErrorOr<IList<IngressoResponse>>>
{
    public async Task<ErrorOr<IList<IngressoResponse>>> Handle(BuscarIngressosPorEventoRequest request, CancellationToken cancellationToken)
    {
        var evento = await eventoRepository.BuscarPorId(request.EventoId, cancellationToken);

        if (evento is null)
        {
            return ErrosIngresso.EventoNaoEncontrado;
        }

        var ingressos = await ingressoRepository.BuscarPorEvento(evento.Id, cancellationToken);

        return ingressos.Select(i => (IngressoResponse)i).ToList();
    }
}