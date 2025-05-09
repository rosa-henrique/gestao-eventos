﻿using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Api.Endpoints.Eventos.Request;
using GestaoEventos.Application.Eventos.Commands.AdicionarEvento;
using GestaoEventos.Application.Eventos.Commands.AdicionarIngresso;
using GestaoEventos.Application.Eventos.Commands.AdicionarSessao;
using GestaoEventos.Application.Eventos.Commands.AlterarEvento;
using GestaoEventos.Application.Eventos.Commands.AlterarIngresso;
using GestaoEventos.Application.Eventos.Commands.AlterarSessao;
using GestaoEventos.Application.Eventos.Commands.AlterarStatusEvento;
using GestaoEventos.Application.Eventos.Commands.CancelarEvento;
using GestaoEventos.Application.Eventos.Commands.RemoverIngresso;
using GestaoEventos.Application.Eventos.Commands.RemoverSessao;
using GestaoEventos.Application.Eventos.Queries.BuscarEvento;
using GestaoEventos.Application.Eventos.Queries.BuscarEventos;
using GestaoEventos.Application.Eventos.Queries.BuscarIngressoPorEvento;
using GestaoEventos.Application.Eventos.Queries.BuscarSessoesPorEvento;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Eventos;

public class EventoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(EndpointSchema.Eventos).WithTags(EndpointSchema.Eventos).RequireAuthorization();

        mapGroup.MapGet(string.Empty, (ISender mediator) =>
        {
            var command = new BuscarEventosQuery();
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).AllowAnonymous();

        mapGroup.MapGet("/{id:guid}", (ISender mediator, Guid id) =>
        {
            var command = new BuscarEventoQuery(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).AllowAnonymous();

        mapGroup.MapGet("/{id:guid}/ingressos", (ISender mediator, Guid id) =>
        {
            var command = new BuscarIngressoPorEventoQuery(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).AllowAnonymous();

        mapGroup.MapGet("/{id:guid}/sessoes", (ISender mediator, Guid id) =>
        {
            var command = new BuscarSessoesPorEventoQuery(id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        }).AllowAnonymous();

        mapGroup.MapPost(string.Empty, (ISender mediator, [FromBody] AdicionarEventoRequest request) =>
        {
            var command = new AdicionarEventoCommand(request.Nome, request.DataHoraInicio, request.DataHoraFim, request.Localizacao, request.CapacidadeMaxima);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created($"{EndpointSchema.Eventos}/{v.Id}", v),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPost("{id:guid}/ingressos", (ISender mediator, Guid id, [FromBody] AdicionarIngressoRequest request) =>
        {
            var command = new AdicionarIngressoCommand(request.Nome, request.Descricao, request.Preco, request.Quantidade, id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created($"{EndpointSchema.Eventos}/{id}/ingressos/{v.Id}", v),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPost("{id:guid}/sessoes", (ISender mediator, Guid id, [FromBody] AdicionarSessaoRequest request) =>
        {
            var command = new AdicionarSessaoCommand(id, request.Nome, request.DataHoraInicio, request.DataHoraFim);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created($"{EndpointSchema.Eventos}/{id}/sessoes/{v.Id}", v),
                ProblemRequest.Resolve);
        });

        mapGroup.MapPut("/{id:guid}", (ISender mediator, Guid id, [FromBody] AlterarEventoRequest request) =>
        {
            var command = new AlterarEventoCommand(id, request.Nome, request.DataHoraInicio, request.DataHoraFim, request.Localizacao, request.CapacidadeMaxima, request.Status);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        });

        mapGroup.MapPut("/{id:guid}/ingressos/{ingressoId:guid}", (ISender mediator, Guid id, Guid ingressoId, [FromBody] AdicionarIngressoRequest request) =>
        {
            var command = new AlterarIngressoCommand(ingressoId, request.Nome, request.Descricao, request.Preco, request.Quantidade, id);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        });

        mapGroup.MapPut("/{id:guid}/sessoes/{sessaoId:guid}", (ISender mediator, Guid id, Guid sessaoId, [FromBody] AdicionarSessaoRequest request) =>
        {
            var command = new AlterarSessaoCommand(id, sessaoId, request.Nome, request.DataHoraInicio, request.DataHoraFim);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        });

        mapGroup.MapPatch("/{id:guid}/status/{status:int}", (ISender mediator, Guid id, int status) =>
        {
            var command = new AlterarStatusEventoCommand(id, status);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
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

        mapGroup.MapDelete("/{id}/ingressos/{ingressoId}", (ISender mediator, Guid id, Guid ingressoId) =>
        {
            var command = new RemoverIngressoCommand(id, ingressoId);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.NoContent(),
                ProblemRequest.Resolve);
        });

        mapGroup.MapDelete("/{id}/sessoes/{sessaoId}", (ISender mediator, Guid id, Guid sessaoId) =>
        {
            var command = new RemoverSessaoCommand(id, sessaoId);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.NoContent(),
                ProblemRequest.Resolve);
        });
    }
}