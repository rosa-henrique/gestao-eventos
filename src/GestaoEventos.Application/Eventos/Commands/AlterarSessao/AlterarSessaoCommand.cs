using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarSessao;

public record AlterarSessaoCommand(Guid EventoId, Guid SessaoId, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim) : IRequest<ErrorOr<Sessao>>
{
}