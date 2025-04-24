using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Api.Endpoints.Compras.Request;
using GestaoEventos.Api.Endpoints.Usuarios.Request;
using GestaoEventos.Application.Compras.Commands.ComprarIngressos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Compras;

public class ComprasEnpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(EndpointSchema.Compras).WithTags(EndpointSchema.Compras);

        mapGroup.MapPost("ingressos/{sessaoId:guid}",
            (ISender mediator, Guid sessaoId, [FromBody] IEnumerable<CompraIngressoRequest> request) =>
            {
                var command =
                    new ComprarIngressosCommand(sessaoId,
                        request.Select(a => new IngressosCompra(a.IngressoId, a.Quantidade)));
                var resultado = mediator.Send(command);

                return resultado.Match(
                    v => Results.Created(),
                    ProblemRequest.Resolve);
            });
    }
}