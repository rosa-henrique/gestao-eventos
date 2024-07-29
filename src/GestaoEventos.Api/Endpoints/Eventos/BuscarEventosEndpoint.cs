﻿using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Application.Eventos.Queries.BuscarEventos;
using GestaoEventos.Contracts.Eventos;
using GestaoEventos.Contracts.Eventos.Adicionar;

using Mapster;

using MediatR;

namespace GestaoEventos.Api.Endpoints.Eventos;

public class BuscarEventosEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(EndpointSchema.Eventos, (ISender mediator) =>
        {
            var command = new BuscarEventosQuery();
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<IEnumerable<EventoDto>>()),
                e => Results.NotFound(e));
        })
        .Produces<IEnumerable<EventoDto>>(StatusCodes.Status201Created);
    }
}