using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventos;

public record BuscarEventosResponse(Guid Id, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim, string Localizacao, int CapacidadeMaxima)
{
    public static implicit operator BuscarEventosResponse(Evento evento)
    {
        return new BuscarEventosResponse(
            evento.Id,
            evento.Nome,
            evento.DataHoraInicio,
            evento.DataHoraFim,
            evento.Localizacao,
            evento.CapacidadeMaxima);
    }
}