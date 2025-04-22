using ErrorOr;

using GestaoEventos.Application.Common.Security;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverIngresso;

[AuthorizeCriadorEvento("EventoId")]
public record RemoverIngressoCommand(Guid EventoId, Guid IngressoId) : IRequest<ErrorOr<Success>>
{
}