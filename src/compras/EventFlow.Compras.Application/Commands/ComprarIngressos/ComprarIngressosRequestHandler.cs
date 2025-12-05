using ErrorOr;

using EventFlow.Compras.Application.Interfaces;
using EventFlow.Compras.Domain;
using EventFlow.Shared.Application.Interfaces;

using MediatR;

namespace EventFlow.Compras.Application.Commands.ComprarIngressos;

public class ComprarIngressosRequestHandler(IIngressoClient ingressoClient, ICompraIngressoRepository repository, IAuthorizationService authorizationService)
    : IRequestHandler<ComprarIngressosRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ComprarIngressosRequest request, CancellationToken cancellationToken)
    {
        var ingressosCompra = request.IngressosCompra;
        var processarItensRequest = ingressosCompra.ToDictionary(e => e.Key.ToString(), e => e.Value);
        var resultado = await ingressoClient.ProcessarItens(processarItensRequest);

        if (resultado.IsError)
        {
            return resultado.Errors;
        }

        var valores = resultado.Value;
        var usuarioId = authorizationService.ObterIdUsuario();
        var itensCompra = valores.Select(v => new ItemCompra
        {
            IngressoId = v.IngressoId,
            EventoId = v.EventoId,
            PrecoUnitario = v.ValorUnitario,
            Quantidade = ingressosCompra[v.IngressoId],
        });

        var compraIngresso = CompraIngresso.Criar(usuarioId, itensCompra.ToList());

        repository.Adicionar(compraIngresso);
        await repository.SalvarAlteracoes(cancellationToken);

        return Result.Success;
    }
}