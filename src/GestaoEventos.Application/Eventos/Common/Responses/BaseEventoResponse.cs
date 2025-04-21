using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Common.Responses;

public record BaseEventoResponse(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    string Status)
{
    public static implicit operator BaseEventoResponse(Evento evento)
        => new(
            evento.Id,
            evento.Nome,
            evento.DataHoraInicio,
            evento.DataHoraFim,
            evento.Localizacao,
            evento.CapacidadeMaxima,
            evento.Status.Name);
}