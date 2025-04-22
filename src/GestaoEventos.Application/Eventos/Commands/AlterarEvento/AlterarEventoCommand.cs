using ErrorOr;

using GestaoEventos.Application.Common.Security;
using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarEvento;

[AuthorizeCriadorEvento("Id")]
public record AlterarEventoCommand(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    int Status) : IRequest<ErrorOr<BaseEventoResponse>>
{
}