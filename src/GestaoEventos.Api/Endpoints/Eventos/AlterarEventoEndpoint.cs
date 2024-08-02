using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Commands.Adicionar;
using GestaoEventos.Application.Eventos.Commands.Alterar;
using GestaoEventos.Contracts.Eventos;
using GestaoEventos.Contracts.Eventos.Adicionar;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Eventos;

public class AlterarEventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut($"{EndpointSchema.Eventos}/{{id}}", (ISender mediator, Guid id, [FromBody] AdicionarEventoRequest request) =>
        {
            var command = new AlterarEventoCommand(id, request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<EventoDto>()),
                ProblemRequest.Resolve);
        })
            .WithTags(EndpointSchema.Eventos)
            .Produces<IEnumerable<EventoDto>>(StatusCodes.Status201Created);
    }
}