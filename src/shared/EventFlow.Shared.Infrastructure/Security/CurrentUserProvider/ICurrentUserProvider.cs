using EventFlow.Shared.Infrastructure.CurrentUserProvider;

namespace EventFlow.Shared.Infrastructure.Security.CurrentUserProvider;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}