using EventFlow.Compras.Api.Abstractions;
using EventFlow.Compras.Application.Commands.ComprarIngressos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace EventFlow.Compras.Api.Endpoints;

public static class ComprarIngressoEndpoint
{
    public static void MapComprarIngressoEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ingressos/comprar", async (ISender sender, [FromBody] IEnumerable<IngressoCompra> ingressoCompras, CancellationToken cancellationToken) =>
        {
            var criarIngressoRequest = new ComprarIngressosRequest(ingressoCompras.Select(e => (e.IngressoId, e.Quantidade)));
            var resultado = await sender.Send(criarIngressoRequest, cancellationToken);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }

    private record IngressoCompra(Guid IngressoId, int Quantidade);
}