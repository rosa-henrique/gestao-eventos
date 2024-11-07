using ErrorOr;

using GestaoEventos.Application.Common.Interfaces;
using GestaoEventos.Domain.Usuarios;

using MediatR;

namespace GestaoEventos.Application.Auth.Login;

public class LoginCommandHandler(IUsuarioRepository repository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await repository.BuscarPorEmail(request.Email);
        if (usuario is null)
        {
            return Error.NotFound(description: "teste");
        }

        var token = jwtTokenGenerator.GenerateToken(usuario.Id, usuario.Nome, usuario.Email, usuario.Permissao);

        return token;
    }
}