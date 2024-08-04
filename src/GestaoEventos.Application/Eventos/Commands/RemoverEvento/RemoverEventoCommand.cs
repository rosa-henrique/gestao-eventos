using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverEvento;

public record RemoverEventoCommand(Guid Id) : IRequest<ErrorOr<Success>>
{
}