using EventFlow.Eventos.Api.Abstractions;
using EventFlow.Eventos.Application.Commands.BuscarPorId;

using MediatR;

namespace EventFlow.Eventos.Api.Endpoints;

public static class BuscarPorIdEndpoint
{
    public static void MapBuscarPorId(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{id}", async (ISender sender, Guid id) =>
        {
            var buscarRequest = new BuscarPorIdRequest(id);
            var resultado = await sender.Send(buscarRequest);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }
}