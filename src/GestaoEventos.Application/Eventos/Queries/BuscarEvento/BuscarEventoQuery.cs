using ErrorOr;

using GestaoEventos.Application.Eventos.Common.Responses;
using GestaoEventos.Domain.Eventos;

using MediatR;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEvento;

public record BuscarEventoQuery(Guid Id) : IRequest<ErrorOr<BaseEventoResponse>>
{
}