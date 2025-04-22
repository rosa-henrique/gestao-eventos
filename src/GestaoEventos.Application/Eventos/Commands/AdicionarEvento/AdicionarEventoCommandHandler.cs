using ErrorOr;

using GestaoEventos.Application.Common.Interfaces;
using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarEvento;

public class AdicionarEventoCommandHandler(IEventoRepository repository, IAuthorizationService authorizationService)
    : IRequestHandler<AdicionarEventoCommand, ErrorOr<BaseEventoResponse>>
{
    public async Task<ErrorOr<BaseEventoResponse>> Handle(AdicionarEventoCommand request, CancellationToken cancellationToken)
    {
        var idUsuario = authorizationService.ObterIdUsuario();
        var resultadoEvento = Evento.Criar(request.Nome, request.DataHoraInicio, request.DataHoraFim,
            request.Localizacao, request.CapacidadeMaxima, idUsuario);

        if (resultadoEvento.IsError)
        {
            return resultadoEvento.Errors;
        }

        var evento = resultadoEvento.Value;

        if (await repository.ObterPorNomeId(evento.Nome, cancellationToken: cancellationToken) is not null)
        {
            return Error.Conflict(description: ErrosEvento.NomeEventoJaExiste);
        }

        repository.Adicionar(evento);
        await repository.SaveChangesAsync(cancellationToken);

        return (BaseEventoResponse)evento;
    }
}