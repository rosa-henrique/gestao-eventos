using EventFlow.Eventos.Api.Abstractions;
using EventFlow.Eventos.Application.Commands.Buscar;

using MediatR;

namespace EventFlow.Eventos.Api.Endpoints;

public static class BuscarEndpoint
{
    public static void MapBuscar(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (ISender sender) =>
        {
            var buscarRequest = new BuscarRequest();
            var resultado = await sender.Send(buscarRequest);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }
}