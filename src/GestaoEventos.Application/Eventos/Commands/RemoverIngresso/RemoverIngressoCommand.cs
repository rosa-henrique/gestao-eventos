using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverIngresso;

public record RemoverIngressoCommand(Guid EventoId, Guid IngressoId) : IRequest<ErrorOr<Success>>
{
}