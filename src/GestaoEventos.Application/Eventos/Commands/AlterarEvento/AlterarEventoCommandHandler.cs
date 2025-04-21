using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarEvento;

public class AlterarEventoCommandHandler(IEventoRepository repository) : IRequestHandler<AlterarEventoCommand, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(AlterarEventoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var resultadoEvento = evento.Atualizar(request.Nome, request.DataHoraInicio, request.DataHoraFim, request.Localizacao, request.CapacidadeMaxima, StatusEvento.FromValue(request.Status));
        if (resultadoEvento.IsError)
        {
            return resultadoEvento.Errors;
        }

        if (await repository.ObterPorNomeId(evento.Nome, evento.Id, cancellationToken) is not null)
        {
            return Error.Conflict(description: ErrosEvento.NomeEventoJaExiste);
        }

        await repository.SaveChangesAsync(cancellationToken);

        return evento;
    }
}