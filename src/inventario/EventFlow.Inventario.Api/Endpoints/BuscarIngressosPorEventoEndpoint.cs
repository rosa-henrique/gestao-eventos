using EventFlow.Inventario.Api.Abstractions;
using EventFlow.Inventario.Application.Commands.BuscarIngressoPorId;
using EventFlow.Inventario.Application.Commands.BuscarIngressosPorEvento;
using EventFlow.Inventario.Application.Commands.CriarIngresso;

using MediatR;

namespace EventFlow.Inventario.Api.Endpoints;

public static class BuscarIngressosPorEventoEndpoint
{
    public static void MapBuscarIngressosPorEventoEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ingressos/evento/{eventiId}", async (ISender sender, Guid eventiId, CancellationToken cancellationToken) =>
        {
            var criarIngressoRequest = new BuscarIngressosPorEventoRequest(eventiId);
            var resultado = await sender.Send(criarIngressoRequest, cancellationToken);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }
}