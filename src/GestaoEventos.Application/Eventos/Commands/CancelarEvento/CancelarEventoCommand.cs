using ErrorOr;

using GestaoEventos.Application.Common.Security;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.CancelarEvento;

[AuthorizeCriadorEvento("Id")]
public record CancelarEventoCommand(Guid Id) : IRequest<ErrorOr<Success>>
{
}