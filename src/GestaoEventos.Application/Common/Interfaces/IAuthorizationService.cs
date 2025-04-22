using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Common.Interfaces;

public interface IAuthorizationService
{
    ErrorOr<Success> AuthorizeCriadorEvento(Evento evento);
    Guid ObterIdUsuario();
}