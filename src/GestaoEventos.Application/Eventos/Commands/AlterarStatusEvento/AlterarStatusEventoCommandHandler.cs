using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarStatusEvento;

public class AlterarStatusEventoCommandHandler(IEventoRepository repository)
    : IRequestHandler<AlterarStatusEventoCommand, ErrorOr<EventoResponse>>
{
    public async Task<ErrorOr<EventoResponse>> Handle(AlterarStatusEventoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var resultadoAtualizarStatus = evento.AtualizarStatus(StatusEvento.FromValue(request.Status));
        if (resultadoAtualizarStatus.IsError)
        {
            return resultadoAtualizarStatus.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return (EventoResponse)evento;
    }
}