using ErrorOr;

using GestaoEventos.Application.Eventos.Commands.RemoverIngresso;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverSessao;

public class RemoverSessaoCommandHandler(IEventoRepository repository) : IRequestHandler<RemoverSessaoCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(RemoverSessaoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var sessao = evento.Sessoes.FirstOrDefault(i => i.Id == request.IngressoId);
        if (sessao is null)
        {
            return Error.NotFound(description: ErrosEvento.SessaoNaoEncontrada);
        }

        var resultadoAlterar = evento.RemoverSessao(sessao);
        if (resultadoAlterar.IsError)
        {
            return resultadoAlterar.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}