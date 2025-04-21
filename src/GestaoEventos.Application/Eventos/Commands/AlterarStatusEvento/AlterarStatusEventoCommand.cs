using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarStatusEvento;

public record AlterarStatusEventoCommand(Guid Id, int Status) : IRequest<ErrorOr<EventoResponse>>
{
}