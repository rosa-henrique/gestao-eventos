using ErrorOr;

using GestaoEventos.Application.Common.Security;
using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarSessao;

[AuthorizeCriadorEvento("EventoId")]
public record AdicionarSessaoCommand(Guid EventoId, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim)
    : IRequest<ErrorOr<SessaoResponse>>
{
}