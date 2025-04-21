using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarIngresso;

public class AlterarIngressoCommandHandler(IEventoRepository repository)
    : IRequestHandler<AlterarIngressoCommand, ErrorOr<IngressoResponse>>
{
    public async Task<ErrorOr<IngressoResponse>> Handle(AlterarIngressoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var ingresso = evento.Ingressos.FirstOrDefault(i => i.Id == request.Id);
        if (ingresso is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        ingresso.Alterar(request.Nome, request.Descricao, request.Preco, request.Quantidade);
        var resultadoAlterar = evento.AtualizarIngresso(ingresso);
        if (resultadoAlterar.IsError)
        {
            return resultadoAlterar.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return (IngressoResponse)ingresso;
    }
}