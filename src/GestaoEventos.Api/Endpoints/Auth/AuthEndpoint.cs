using ErrorOr;

using GestaoEventos.Api.Abstractions;
using GestaoEventos.Api.Endpoints.Auth.Request;
using GestaoEventos.Application.Auth.Login;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace GestaoEventos.Api.Endpoints.Auth;

public class AuthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup(EndpointSchema.Auth).WithTags(EndpointSchema.Auth);

        mapGroup.MapPost("login", (ISender mediator, [FromBody] LoginRequest request) =>
        {
            var command = new LoginCommand(request.Email, request.Senha);
            var resultado = mediator.Send(command);

            return resultado.Match(
                Results.Ok,
                ProblemRequest.Resolve);
        });
    }
}