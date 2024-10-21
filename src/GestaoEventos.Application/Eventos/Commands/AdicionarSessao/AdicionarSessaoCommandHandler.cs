using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarSessao;

public class AdicionarSessaoCommandHandler(IEventoRepository repository) : IRequestHandler<AdicionarSessaoCommand, ErrorOr<Sessao>>
{
    public async Task<ErrorOr<Sessao>> Handle(AdicionarSessaoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var criarSessaoResult = Sessao.Criar(request.Nome, request.DataHoraInicio, request.DataHoraFim);
        if (criarSessaoResult.IsError)
        {
            return criarSessaoResult.Errors;
        }

        var adicionarSessaoResult = evento.AdicionarSessao(criarSessaoResult.Value);
        if (adicionarSessaoResult.IsError)
        {
            return adicionarSessaoResult.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return criarSessaoResult;
    }
}