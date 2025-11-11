using ErrorOr;

using EventFlow.Eventos.Application.Enums;
using EventFlow.Eventos.Application.Response;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Alterar;

public record AlterarRequest(Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    StatusEvento? Status) : IRequest<ErrorOr<EventoResponse>>;