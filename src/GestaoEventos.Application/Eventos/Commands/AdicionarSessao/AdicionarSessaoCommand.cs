using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarSessao;

public record AdicionarSessaoCommand(Guid EventoId, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim) : IRequest<ErrorOr<Sessao>>
{
}