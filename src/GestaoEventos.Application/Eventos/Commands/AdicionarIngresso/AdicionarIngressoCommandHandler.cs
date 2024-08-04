using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarIngresso;

public class AdicionarIngressoCommandHandler(IEventoRepository repository) : IRequestHandler<AdicionarIngressoCommand, ErrorOr<Ingresso>>
{
    public async Task<ErrorOr<Ingresso>> Handle(AdicionarIngressoCommand request, CancellationToken cancellationToken)
    {
        var evento = await repository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return Error.NotFound(description: ErrosEvento.EventoNaoEncontrado);
        }

        var ingresso = Ingresso.Criar(request.Nome, request.Descricao, request.Preco, request.Quantidade);

        var resultadoAdicionarIngresso = evento.AdicionarIngresso(ingresso);
        if (resultadoAdicionarIngresso.IsError)
        {
            return resultadoAdicionarIngresso.Errors;
        }

        await repository.SaveChangesAsync(cancellationToken);

        return ingresso;
    }
}