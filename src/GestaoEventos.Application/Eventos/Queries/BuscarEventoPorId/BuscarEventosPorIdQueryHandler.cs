using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventoPorId;

public class BuscarEventosPorIdQueryHandler(IEventoRepository repository) : IRequestHandler<BuscarEventosPorIdQuery, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(BuscarEventosPorIdQuery request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id);

        return evento is not null ? evento : Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
    }
}