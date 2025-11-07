using ErrorOr;

using EventFlow.Eventos.Application.Response;
using EventFlow.Eventos.Domain;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.BuscarPorId;

public class BuscarPorIdRequestHandler(IEventoRepository repository) : IRequestHandler<BuscarPorIdRequest, ErrorOr.ErrorOr<EventoResponse>>
{
    public async Task<ErrorOr<EventoResponse>> Handle(BuscarPorIdRequest request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken);

        return evento is null ? ErrosEvento.EventoNaoEncontrado : (EventoResponse)evento;
    }
}