using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Queries.BuscarEventoPorId;
using GestaoEventos.Contracts.Eventos;

using Mapster;

using MediatR;

namespace GestaoEventos.Api.Endpoints.Eventos;

public class BuscarEventoPorIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{EndpointSchema.Eventos}/{{id}}", (ISender mediator, Guid id) =>
        {
            var command = new BuscarEventosPorIdQuery(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<EventoDto>()),
                ProblemRequest.Resolve);
        })
        .Produces<IEnumerable<EventoDto>>(StatusCodes.Status201Created);
    }
}