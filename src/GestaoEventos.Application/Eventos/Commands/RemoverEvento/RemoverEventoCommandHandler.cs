using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverEvento;

public class RemoverEventoCommandHandler(IEventoRepository repository) : IRequestHandler<RemoverEventoCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RemoverEventoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id);
        if (evento is null)
        {
            return Error.Failure(description: ErrosEvento.EventoNaoEncontrado);
        }

        var validaRemover = evento.Cancelar();
        if (validaRemover.IsError)
        {
            return validaRemover.Errors;
        }

        await repository.Deletar(evento);

        return Result.Success;
    }
}