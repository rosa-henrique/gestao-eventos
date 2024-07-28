using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.Adicionar;

public class AdicionarEventoCommandHandler(IEventoRepository repository, EventoDomainService domainService) : IRequestHandler<AdicionarEventoCommand, ErrorOr<Evento>>
{
    public async Task<ErrorOr<Evento>> Handle(AdicionarEventoCommand request, CancellationToken cancellationToken)
    {
        if ((await repository.ObterPorNomeId(request.Nome)) is not null)
        {
            return Error.Conflict(description: ErrosEvento.NomeEventoJaExiste);
        }

        var resultadoEvento = Evento.Criar(request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima);

        if (resultadoEvento.IsError)
        {
            return resultadoEvento.Errors;
        }

        await domainService.CriarEvento(resultadoEvento.Value);

        return resultadoEvento.Value;
    }
}