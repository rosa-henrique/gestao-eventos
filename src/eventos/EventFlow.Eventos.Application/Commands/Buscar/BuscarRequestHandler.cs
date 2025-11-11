using ErrorOr;

using EventFlow.Eventos.Application.Response;
using EventFlow.Eventos.Domain;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Buscar;

public class BuscarRequestHandler(IEventoRepository repository) : IRequestHandler<BuscarRequest, ErrorOr<IEnumerable<EventoResponse>>>
{
    public async Task<ErrorOr<IEnumerable<EventoResponse>>> Handle(BuscarRequest request, CancellationToken cancellationToken)
    {
        var eventos = await repository.Buscar(cancellationToken);

        return eventos.Select(e => (EventoResponse)e) as dynamic;
    }
}