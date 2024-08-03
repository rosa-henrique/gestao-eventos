using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Queries.BuscarEventos;

using Mapster;

using MediatR;

namespace GestaoEventos.Api.Endpoints.Eventos.BuscarEventos;

public class BuscarEventosEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSchema.Eventos, (ISender mediator) =>
        {
            var command = new BuscarEventosQuery();
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<IEnumerable<BuscarEventosResponse>>()),
                ProblemRequest.Resolve);
        })
            .WithTags(EndpointSchema.Eventos)
            .Produces<IEnumerable<BuscarEventosResponse>>(StatusCodes.Status201Created);
    }
}