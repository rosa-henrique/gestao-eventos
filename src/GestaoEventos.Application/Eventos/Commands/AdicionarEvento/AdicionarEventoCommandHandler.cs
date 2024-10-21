using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarEvento;

public class AdicionarEventoCommandHandler(IEventoRepository repository) : IRequestHandler<AdicionarEventoCommand, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(AdicionarEventoCommand request, CancellationToken cancellationToken)
    {
        var resultadoEvento = Evento.Criar(request.Nome, request.DataHoraInicio, request.DataHoraFim, request.Localizacao, request.CapacidadeMaxima);

        if (resultadoEvento.IsError)
        {
            return resultadoEvento.Errors;
        }

        var evento = resultadoEvento.Value;

        if (await repository.ObterPorNomeId(evento.Detalhes.Nome, cancellationToken: cancellationToken) is not null)
        {
            return Error.Conflict(description: ErrosEvento.NomeEventoJaExiste);
        }

        repository.Adicionar(evento);
        await repository.SaveChangesAsync(cancellationToken);

        return evento;
    }
}