using ErrorOr;

using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AlterarEvento;

public record AlterarEventoCommand(Guid Id, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim, string Localizacao, int CapacidadeMaxima, int Status) : IRequest<ErrorOr<Evento>>
{
}