using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Commands.Alterar;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Eventos.AlterarEvento;

public class AlterarEventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut($"{EndpointSchema.Eventos}/{{id}}", (ISender mediator, Guid id, [FromBody] AlterarEventoRequest request) =>
        {
            var command = new AlterarEventoCommand(id, request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima, request.Status);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<AlterarEventoRequest>()),
                ProblemRequest.Resolve);
        })
            .WithTags(EndpointSchema.Eventos)
            .Produces<IEnumerable<AlterarEventoRequest>>(StatusCodes.Status201Created);
    }
}