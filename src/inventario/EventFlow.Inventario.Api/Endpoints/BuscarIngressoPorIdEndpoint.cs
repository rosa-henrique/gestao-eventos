using EventFlow.Inventario.Api.Abstractions;
using EventFlow.Inventario.Application.Commands.BuscarIngressoPorId;
using EventFlow.Inventario.Application.Commands.CriarIngresso;

using MediatR;

namespace EventFlow.Inventario.Api.Endpoints;

public static class BuscarIngressoPorIdEndpoint
{
    public static void MapBuscarIngressoPorIdEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ingressos/{id}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var criarIngressoRequest = new BuscarIngressoPorIdRequest(id);
            var resultado = await sender.Send(criarIngressoRequest, cancellationToken);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }
}