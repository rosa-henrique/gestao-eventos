using EventFlow.Shared.Application.Interfaces;
using EventFlow.Shared.Infrastructure.CurrentUserProvider;
using EventFlow.Shared.Infrastructure.Security.CurrentUserProvider;

namespace EventFlow.Shared.Infrastructure.Security;

public class AuthorizationService(ICurrentUserProvider currentUserProvider) : IAuthorizationService
{
    public Guid ObterIdUsuario()
        => currentUserProvider.GetCurrentUser().Id;
}