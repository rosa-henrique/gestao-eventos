using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEvento;

public class BuscarEventosQueryHandler(IEventoRepository repository) : IRequestHandler<BuscarEventosQuery, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(BuscarEventosQuery request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id);

        return evento is not null ? evento : Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
    }
}