using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarStatusEvento;

public class AlterarStatusEventoCommandHandler(IEventoRepository repository) : IRequestHandler<AlterarStatusEventoCommand, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(AlterarStatusEventoCommand request, CancellationToken cancellationToken)
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

        return evento;
    }
}