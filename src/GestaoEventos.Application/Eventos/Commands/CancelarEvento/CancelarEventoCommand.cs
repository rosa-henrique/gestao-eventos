using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.CancelarEvento;

public record CancelarEventoCommand(Guid Id) : IRequest<ErrorOr<Success>>
{
}