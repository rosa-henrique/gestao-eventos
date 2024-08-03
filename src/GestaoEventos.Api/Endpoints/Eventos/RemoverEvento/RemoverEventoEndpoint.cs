using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Commands.Remover;

using MediatR;

namespace GestaoEventos.Api.Endpoints.Eventos.RemoverEvento;

public class RemoverEventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete($"{EndpointSchema.Eventos}/{{id}}", (ISender mediator, Guid id) =>
        {
            var command = new RemoverEventoCommand(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.NoContent(),
                ProblemRequest.Resolve);
        })
            .WithTags(EndpointSchema.Eventos)
            .Produces(StatusCodes.Status204NoContent);
    }
}