using EventFlow.Shared.Infrastructure.CurrentUserProvider;

namespace EventFlow.Shared.Application.Interfaces;

public interface IAuthorizationService
{
    Guid ObterIdUsuario();
}