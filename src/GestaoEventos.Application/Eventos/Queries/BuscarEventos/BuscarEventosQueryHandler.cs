using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventos;

public class BuscarEventosQueryHandler(IEventoRepository repository) : IRequestHandler<BuscarEventosQuery, ErrorOr<IEnumerable<BuscarEventosResponse>>>
{
    public async Task<ErrorOr<IEnumerable<BuscarEventosResponse>>> Handle(BuscarEventosQuery request, CancellationToken cancellationToken)
    {
        var eventos = await repository.Buscar(cancellationToken);

        return eventos.Select(e => (BuscarEventosResponse)e).ToList();
    }
}