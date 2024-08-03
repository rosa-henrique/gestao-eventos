using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.Alterar;

public class AlterarEventoCommandHandler(IEventoRepository repository) : IRequestHandler<AlterarEventoCommand, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(AlterarEventoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.Id);
        if (evento is null)
        {
            return Error.Conflict(description: ErrosEvento.EventoNaoEncontrado);
        }

        var resultadoEvento = evento.Atualizar(request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima, StatusEvento.FromValue(request.Status));
        if (resultadoEvento.IsError)
        {
            return resultadoEvento.Errors;
        }

        if ((await repository.ObterPorNomeId(evento.Detalhes.Nome, evento.Id)) is not null)
        {
            return Error.Conflict(description: ErrosEvento.NomeEventoJaExiste);
        }

        await repository.Alterar(evento);

        return evento;
    }
}