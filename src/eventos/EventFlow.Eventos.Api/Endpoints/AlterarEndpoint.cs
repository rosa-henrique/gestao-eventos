using EventFlow.Eventos.Api.Abstractions;
using EventFlow.Eventos.Application.Commands.Alterar;
using EventFlow.Eventos.Application.Enums;

using MediatR;

namespace EventFlow.Eventos.Api.Endpoints;

public static class AlterarEndpoint
{
    public static void MapAlterar(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/{id}", async (ISender sender, Guid id, Request request) =>
        {
            var alterarRequest = new AlterarRequest(id,
                request.Nome,
                request.DataHoraInicio,
                request.DataHoraFim,
                request.Localizacao,
                request.CapacidadeMaxima,
                request.Status);
            var resultado = await sender.Send(alterarRequest);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).RequireAuthorization();
    }

    private record Request(string Nome,
        DateTime DataHoraInicio,
        DateTime DataHoraFim,
        string Localizacao,
        int CapacidadeMaxima,
        StatusEvento? Status);
}