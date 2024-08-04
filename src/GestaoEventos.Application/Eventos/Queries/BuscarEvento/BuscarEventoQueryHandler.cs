using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEvento;

public class BuscarEventoQueryHandler(IEventoRepository repository) : IRequestHandler<BuscarEventoQuery, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(BuscarEventoQuery request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken, false);

        return evento is not null ? evento : Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
    }
}