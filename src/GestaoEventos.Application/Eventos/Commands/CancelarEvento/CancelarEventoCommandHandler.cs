using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.CancelarEvento;

public class CancelarEventoCommandHandler(IEventoRepository repository) : IRequestHandler<CancelarEventoCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CancelarEventoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var validaRemover = evento.Cancelar();
        if (validaRemover.IsError)
        {
            return validaRemover.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}