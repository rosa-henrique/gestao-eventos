using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverIngresso;

public class RemoverIngressoCommandHandler(IEventoRepository repository) : IRequestHandler<RemoverIngressoCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RemoverIngressoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var ingresso = evento.Ingressos.FirstOrDefault(i => i.Id == request.IngressoId);
        if (ingresso is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var resultadoAlterar = evento.RemoverIngresso(ingresso);
        if (resultadoAlterar.IsError)
        {
            return resultadoAlterar.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}