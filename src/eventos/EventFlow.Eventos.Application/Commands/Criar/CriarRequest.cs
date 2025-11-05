using ErrorOr;

using EventFlow.Eventos.Application.Response;

using MediatR;

namespace EventFlow.Eventos.Application.Commands.Criar;

public record CriarRequest(string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima) : IRequest<ErrorOr<EventoResponse>>;