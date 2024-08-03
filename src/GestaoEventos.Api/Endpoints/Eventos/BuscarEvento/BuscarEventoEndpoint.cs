using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Queries.BuscarEvento;

using Mapster;

using MediatR;

namespace GestaoEventos.Api.Endpoints.Eventos.BuscarEvento;

public class BuscarEventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{EndpointSchema.Eventos}/{{id}}", (ISender mediator, Guid id) =>
        {
            var command = new BuscarEventosQuery(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<BuscarEventoResponse>()),
                ProblemRequest.Resolve);
        })
            .WithTags(EndpointSchema.Eventos)
            .Produces<IEnumerable<BuscarEventoResponse>>(StatusCodes.Status201Created);
    }
}