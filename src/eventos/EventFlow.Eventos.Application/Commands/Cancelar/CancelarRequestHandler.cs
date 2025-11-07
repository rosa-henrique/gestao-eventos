using ErrorOr;

using EventFlow.Eventos.Application.Response;
using EventFlow.Eventos.Domain;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Cancelar;

public class CancelarRequestHandler(IEventoRepository repository) : IRequestHandler<CancelarRequest, ErrorOr<EventoResponse>>
{
    public async Task<ErrorOr<EventoResponse>> Handle(CancelarRequest request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken);
        if (evento is null)
        {
            return ErrosEvento.EventoNaoEncontrado;
        }

        var resultadoCancelar = evento.Cancelar();
        if (resultadoCancelar.IsError)
        {
            return resultadoCancelar.Errors;
        }

        repository.Alterar(evento);
        await repository.SalvarAlteracoes(cancellationToken);

        return (EventoResponse)evento;
    }
}