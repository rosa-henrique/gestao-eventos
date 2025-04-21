using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarIngresso;

public class AdicionarIngressoCommandHandler(IEventoRepository repository)
    : IRequestHandler<AdicionarIngressoCommand, ErrorOr<IngressoResponse>>
{
    public async Task<ErrorOr<IngressoResponse>> Handle(AdicionarIngressoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var ingresso = Ingresso.Criar(request.Nome, request.Descricao, request.Preco, request.Quantidade);

        var resultadoAdicionarIngresso = evento.AdicionarIngresso(ingresso.Value);
        if (resultadoAdicionarIngresso.IsError)
        {
            return resultadoAdicionarIngresso.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return (IngressoResponse)ingresso.Value;
    }
}