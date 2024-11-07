using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<string>>
{
    public Task<ErrorOr<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}