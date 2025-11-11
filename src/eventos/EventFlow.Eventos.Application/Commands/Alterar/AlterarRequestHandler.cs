using ErrorOr;

using EventFlow.Eventos.Application.Response;
using EventFlow.Eventos.Domain;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Alterar;

public class AlterarRequestHandler(IEventoRepository repository) : IRequestHandler<AlterarRequest, ErrorOr<EventoResponse>>
{
    public async Task<ErrorOr<EventoResponse>> Handle(AlterarRequest request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken);
        if (evento is null)
        {
            return ErrosEvento.EventoNaoEncontrado;
        }

        var status = StatusEvento.FromName(request.Status!.ToString());
        var resultadoAtualizar = evento.Atualizar(request.Nome, request.DataHoraInicio, request.DataHoraFim, request.Localizacao, request.CapacidadeMaxima, status);

        if (resultadoAtualizar.IsError)
        {
            return resultadoAtualizar.Errors;
        }

        repository.Alterar(evento);
        await repository.SalvarAlteracoes(cancellationToken);

        return (EventoResponse)evento;
    }
}