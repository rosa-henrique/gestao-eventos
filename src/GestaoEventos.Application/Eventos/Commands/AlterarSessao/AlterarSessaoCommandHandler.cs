using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarSessao;

public class AlterarSessaoCommandHandler(IEventoRepository repository) : IRequestHandler<AlterarSessaoCommand, ErrorOr<Sessao>>
{
    public async Task<ErrorOr<Sessao>> Handle(AlterarSessaoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var sessao = evento.Sessoes.FirstOrDefault(i => i.Id == request.SessaoId);
        if (sessao is null)
        {
            return Error.NotFound(description: ErrosEvento.SessaoNaoEncontrada);
        }

        var resultadoAlterarSessao = sessao.Alterar(request.Nome, request.DataHoraInicio, request.DataHoraFim);
        if (resultadoAlterarSessao.IsError)
        {
            return resultadoAlterarSessao.Errors;
        }

        var resultadoAtualizarSessao = evento.AtualizarSessao(sessao);
        if (resultadoAtualizarSessao.IsError)
        {
            return resultadoAtualizarSessao.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return sessao;
    }
}