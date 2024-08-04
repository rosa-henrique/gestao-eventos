using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Api.Endpoints.Eventos.Request;
using GestaoEventos.Api.Endpoints.Eventos.Response;
using GestaoEventos.Application.Eventos.Commands.AdicionarEvento;
using GestaoEventos.Application.Eventos.Commands.AdicionarIngresso;
using GestaoEventos.Application.Eventos.Commands.AlterarEvento;
using GestaoEventos.Application.Eventos.Commands.AlterarStatusEvento;
using GestaoEventos.Application.Eventos.Commands.CancelarEvento;
using GestaoEventos.Application.Eventos.Queries.BuscarEvento;
using GestaoEventos.Application.Eventos.Queries.BuscarEventos;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Eventos;

public class EventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(EndpointSchema.Eventos).WithTags(EndpointSchema.Eventos);

        mapGroup.MapGet(string.Empty, (ISender mediator) =>
        {
            var command = new BuscarEventosQuery();
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<IEnumerable<EventoResponse>>()),
                ProblemRequest.Resolve);
        });

        mapGroup.MapGet("/{id:guid}", (ISender mediator, Guid id) =>
        {
            var command = new BuscarEventoQuery(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<EventoResponse>()),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPost(string.Empty, (ISender mediator, [FromBody] AdicionarEventoRequest request) =>
        {
            var command = new AdicionarEventoCommand(request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created($"{EndpointSchema.Eventos}/{v.Id}", v.Adapt<EventoResponse>()),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPost("{id:guid}/ingresso", (ISender mediator, Guid id, [FromBody] AdicionarIngressoRequest request) =>
        {
            var command = new AdicionarIngressoCommand(request.Nome, request.Descricao, request.Preco, request.Quantidade, id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPut("/{id:guid}", (ISender mediator, Guid id, [FromBody] AlterarEventoRequest request) =>
        {
            var command = new AlterarEventoCommand(id, request.Nome, request.DataHora, request.Localizacao, request.CapacidadeMaxima, request.Status);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<EventoResponse>()),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPut("/{id:guid}/status/{status:int}", (ISender mediator, Guid id, int status) =>
        {
            var command = new AlterarStatusEventoCommand(id, status);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Ok(v.Adapt<EventoResponse>()),
                ProblemRequest.Resolve);
        });

        mapGroup.MapDelete("/{id}", (ISender mediator, Guid id) =>
        {
            var command = new CancelarEventoCommand(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.NoContent(),
                ProblemRequest.Resolve);
        });
    }
}