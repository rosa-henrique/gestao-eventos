using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventos;

public record BuscarEventosResult(Guid Id, string Nome, DateTime DataHoraInicio, DateTime DataHoraFim, string Localizacao, int CapacidadeMaxima)
{
    public static implicit operator BuscarEventosResult(Evento evento)
    {
        return new BuscarEventosResult(
            evento.Id,
            evento.Nome,
            evento.DataHoraInicio,
            evento.DataHoraFim,
            evento.Localizacao,
            evento.CapacidadeMaxima);
    }
}