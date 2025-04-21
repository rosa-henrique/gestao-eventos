using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Common.Responses;

public record EventoResponse(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    string Status)
{
    public static implicit operator EventoResponse(Evento evento)
    {
        return new EventoResponse(
            evento.Id,
            evento.Nome,
            evento.DataHoraInicio,
            evento.DataHoraFim,
            evento.Localizacao,
            evento.CapacidadeMaxima,
            evento.Status.Name);
    }
}