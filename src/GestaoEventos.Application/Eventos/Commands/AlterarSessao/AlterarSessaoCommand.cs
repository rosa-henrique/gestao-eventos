using ErrorOr;

using GestaoEventos.Application.Common.Security;
using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarSessao;

[AuthorizeCriadorEvento("EventoId")]
public record AlterarSessaoCommand(
    Guid EventoId,
    Guid SessaoId,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim) : IRequest<ErrorOr<SessaoResponse>>
{
}