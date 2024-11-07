using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Auth.Login;

public record LoginCommand(string Email, string Senha) : IRequest<ErrorOr<string>>
{
}