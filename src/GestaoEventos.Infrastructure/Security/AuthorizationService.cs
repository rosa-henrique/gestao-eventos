using ErrorOr;

using GestaoEventos.Application.Common.Interfaces;
using GestaoEventos.Domain.Eventos;
using GestaoEventos.Infrastructure.Security.CurrentUserProvider;

namespace GestaoEventos.Infrastructure.Security;

public class AuthorizationService(ICurrentUserProvider _currentUserProvider) : IAuthorizationService
{
    public ErrorOr<Success> AuthorizeCriadorEvento(Evento evento)
    {
        var currentUser = _currentUserProvider.GetCurrentUser();

        return currentUser.Id == evento.CriadoPor
            ? Result.Success
            : Error.Unauthorized(description: ErrosEvento.UsuarioNaoAutorizado);
    }

    public Guid ObterIdUsuario()
        => _currentUserProvider.GetCurrentUser().Id;
}