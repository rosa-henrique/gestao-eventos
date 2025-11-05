using EventFlow.Eventos.Domain;

namespace EventFlow.Eventos.Application.Response;

public record EventoResponse(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima)
{
    public static implicit operator EventoResponse(Evento evento)
        => new(evento.Id, evento.Nome, evento.DataHoraInicio, evento.DataHoraFim,  evento.Localizacao, evento.CapacidadeMaxima);
}