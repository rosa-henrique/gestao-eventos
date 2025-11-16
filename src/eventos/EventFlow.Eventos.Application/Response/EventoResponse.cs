using EventFlow.Eventos.Domain;

using StatusEvento = EventFlow.Eventos.Application.Enums.StatusEvento;

namespace EventFlow.Eventos.Application.Response;

public record EventoResponse(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    StatusEvento StatusEvento)
{
    public static implicit operator EventoResponse(Evento evento)
        => new(evento.Id, evento.Nome, evento.DataHoraInicio, evento.DataHoraFim,  evento.Localizacao, evento.CapacidadeMaxima, Enum.Parse<StatusEvento>(evento.Status.Name));
}