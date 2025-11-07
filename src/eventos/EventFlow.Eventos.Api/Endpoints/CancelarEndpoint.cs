using EventFlow.Eventos.Api.Abstractions;
using EventFlow.Eventos.Application.Commands.Alterar;
using EventFlow.Eventos.Application.Commands.Cancelar;

using MediatR;

namespace EventFlow.Eventos.Api.Endpoints;

public static class CancelarEndpoint
{
    public static void MapCancelar(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatch("/{id}/cancelar", async (ISender sender, Guid id) =>
        {
            var cancelarRequest = new CancelarRequest(id);
            var resultado = await sender.Send(cancelarRequest);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }
}