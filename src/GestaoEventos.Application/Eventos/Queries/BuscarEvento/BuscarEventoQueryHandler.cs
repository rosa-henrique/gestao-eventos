using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEvento;

public class BuscarEventoQueryHandler(IEventoRepository repository) : IRequestHandler<BuscarEventoQuery, ErrorOr<BaseEventoResponse>>
{
    public async Task<ErrorOr<BaseEventoResponse>> Handle(BuscarEventoQuery request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken, false);

        return evento is not null ? (BaseEventoResponse)evento : Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
    }
}