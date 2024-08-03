using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Commands.Adicionar;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Eventos.AdicionarEvento;

public class AdicionarEventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(EndpointSchema.Eventos, (ISender mediator, [FromBody] AdicionarEventoRequest request) =>
        {
            var command = new AdicionarEventoCommand(request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created($"{EndpointSchema.Eventos}/{v.Id}", v.Adapt<AdicionarEventoResponse>()),
                ProblemRequest.Resolve);
        })
            .WithTags(EndpointSchema.Eventos)
            .Produces<AdicionarEventoResponse>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}