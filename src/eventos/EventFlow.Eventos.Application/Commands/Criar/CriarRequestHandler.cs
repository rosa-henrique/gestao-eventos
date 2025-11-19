using ErrorOr;

using EventFlow.Eventos.Application.Response;
using EventFlow.Eventos.Domain;
using EventFlow.Shared.Application.Interfaces;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Criar;

public class CriarRequestHandler(IEventoRepository repository, IAuthorizationService authorizationService) : IRequestHandler<CriarRequest, ErrorOr<EventoResponse>>
{
    public async Task<ErrorOr<EventoResponse>> Handle(CriarRequest request, CancellationToken cancellationToken)
    {
        var idUsuario = authorizationService.ObterIdUsuario();
        var resultadoEvento = Evento.Criar(request.Nome, request.DataHoraInicio, request.DataHoraFim,
            request.Localizacao, request.CapacidadeMaxima, idUsuario);

        if (resultadoEvento.IsError)
        {
            return resultadoEvento.Errors;
        }

        var evento = resultadoEvento.Value;

        repository.Adicionar(evento);
        await repository.SalvarAlteracoes(cancellationToken);

        return (EventoResponse)evento;
    }
}