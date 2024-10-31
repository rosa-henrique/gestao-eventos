using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Api.Endpoints.Usuarios.Request;
using GestaoEventos.Application.Usuarios.CriarUsuario;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Usuarios;

public class UsuarioEnpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(EndpointSchema.Usuarios).WithTags(EndpointSchema.Usuarios);

        mapGroup.MapPost(string.Empty, (ISender mediator, [FromBody] CriarUsuarioRequest request) =>
        {
            var command = new CriarUsuarioCommand(request.Nome, request.Email, request.Permissao);
            var resultado = mediator.Send(command);

            return resultado.Match(
                v => Results.Created(),
                ProblemRequest.Resolve);
        });
    }
}