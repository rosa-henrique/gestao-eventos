using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.Adicionar;

public class AdicionarEventoCommandHandler(IEventoRepository repository) : IRequestHandler<AdicionarEventoCommand, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(AdicionarEventoCommand request, CancellationToken cancellationToken)
    {
        if ((await repository.ObterPorNomeId(request.Nome)) is not null)
        {
            return Error.Conflict(description: ErrosEvento.NomeEventoJaExiste);
        }

        var evento = new Evento(request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima);

        var resultadoValidacao = EventoDomainService.ValidarEvento(evento);

        if (resultadoValidacao.IsError)
        {
            return resultadoValidacao.Errors;
        }

        await repository.Adicionar(evento);

        return evento;
    }
}