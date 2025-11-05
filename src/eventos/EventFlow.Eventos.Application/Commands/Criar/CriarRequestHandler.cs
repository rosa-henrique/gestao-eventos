using ErrorOr;

using EventFlow.Eventos.Application.Response;
using EventFlow.Eventos.Domain;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Criar;

public class CriarRequestHandler(IEventoRepository repository) : IRequestHandler<CriarRequest, ErrorOr<EventoResponse>>
{
    public async Task<ErrorOr<EventoResponse>> Handle(CriarRequest request, CancellationToken cancellationToken)
    {
        var resultadoEvento = Evento.Criar(request.Nome, request.DataHoraInicio, request.DataHoraFim,
            request.Localizacao, request.CapacidadeMaxima, Guid.Empty);

        if (resultadoEvento.IsError)
        {
            return Task.FromResult(resultadoEvento.Errors as dynamic);
        }

        var evento = resultadoEvento.Value;

        repository.Adicionar(evento);
        await repository.SalvarAlteracoes(cancellationToken);

        return (EventoResponse)evento;
    }
}