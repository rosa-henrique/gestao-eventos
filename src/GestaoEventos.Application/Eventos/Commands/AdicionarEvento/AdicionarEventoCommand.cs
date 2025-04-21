using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Commands.AdicionarEvento;

public record AdicionarEventoCommand(
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima) : IRequest<ErrorOr<BaseEventoResponse>>
{
}