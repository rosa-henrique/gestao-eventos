using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarSessao;

public class AdicionarSessaoCommandHandler(IEventoRepository repository)
    : IRequestHandler<AdicionarSessaoCommand, ErrorOr<SessaoResponse>>
{
    public async Task<ErrorOr<SessaoResponse>> Handle(AdicionarSessaoCommand request, CancellationToken cancellationToken)
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

        return (SessaoResponse)criarSessaoResult.Value;
    }
}