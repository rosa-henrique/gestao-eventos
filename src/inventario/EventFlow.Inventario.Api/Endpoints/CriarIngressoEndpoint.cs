using EventFlow.Inventario.Api.Abstractions;
using EventFlow.Inventario.Application.Commands.CriarIngresso;

using MediatR;

namespace EventFlow.Inventario.Api.Endpoints;

public static class CriarIngressoEndpoint
{
    public static void MapCriarIngressoEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ingressos", async (ISender sender, Request request, CancellationToken cancellationToken) =>
        {
            var criarIngressoRequest = new CriarIngressoRequest(request.Nome,
                request.Descricao,
                request.Preco,
                request.QuantidadeTotal,
                request.EventoId);
            var resultado = await sender.Send(criarIngressoRequest, cancellationToken);

            return resultado.Match(
                v => Results.Created($"/ingressos/{v.Id}", v),
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }

    private record Request(string Nome, string Descricao, decimal Preco, int QuantidadeTotal, Guid EventoId);
}