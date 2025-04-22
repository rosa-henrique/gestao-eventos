using ErrorOr;

using GestaoEventos.Application.Common.Security;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverSessao;

[AuthorizeCriadorEvento("EventoId")]
public record RemoverSessaoCommand(Guid EventoId, Guid IngressoId) : IRequest<ErrorOr<Success>>
{
}