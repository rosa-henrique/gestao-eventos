using EventFlow.Eventos.Api.Abstractions;
using EventFlow.Eventos.Application.Commands.Criar;

using MediatR;

namespace EventFlow.Eventos.Api.Endpoints;

public static class CriarEndpoint
{
    public static void MapCriar(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", async (ISender sender, Request request) =>
        {
            var criarRequest = new CriarRequest(request.Nome,
                                                request.DataHoraInicio,
                                                request.DataHoraFim,
                                                request.Localizacao,
                                                request.CapacidadeMaxima);
            var resultado = await sender.Send(criarRequest);

            return resultado.Match(
                v => Results.Created($"/{v.Id}", v),
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }

    private record Request(string Nome,
        DateTime DataHoraInicio,
        DateTime DataHoraFim,
        string Localizacao,
        int CapacidadeMaxima);
}