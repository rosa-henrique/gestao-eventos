using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventos;

public class BuscarEventosQueryHandler(IEventoRepository repository) : IRequestHandler<BuscarEventosQuery, ErrorOr<IEnumerable<BuscarEventosResult>>>
{
    public async Task<ErrorOr<IEnumerable<BuscarEventosResult>>> Handle(BuscarEventosQuery request, CancellationToken cancellationToken)
    {
        var eventos = await repository.Buscar(cancellationToken);

        return eventos.Select(e => (BuscarEventosResult)e).ToList();
    }
}