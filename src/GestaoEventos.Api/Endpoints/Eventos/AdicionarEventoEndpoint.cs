using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Commands.Adicionar;
using GestaoEventos.Contracts.Eventos.Adicionar;

using MediatR;

namespace GestaoEventos.Api.Endpoints.Eventos;

public class AdicionarEventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(EndpointSchema.Eventos, (ISender mediator, AdicionarEventoRequest request) =>
        {
            var command = new AdicionarEventoCommand(request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created($"{EndpointSchema.Eventos}/{v.Id}", v),
                e => Results.BadRequest(e));
        })
        .Produces(StatusCodes.Status201Created)
        .Produces<Error>(StatusCodes.Status400BadRequest);
    }
}