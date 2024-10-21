using ErrorOr;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.RemoverSessao;

public record RemoverSessaoCommand(Guid EventoId, Guid IngressoId) : IRequest<ErrorOr<Success>>
{
}