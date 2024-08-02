using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.Remover;

public record RemoverEventoCommand(Guid Id) : IRequest<ErrorOr<Success>>
{
}