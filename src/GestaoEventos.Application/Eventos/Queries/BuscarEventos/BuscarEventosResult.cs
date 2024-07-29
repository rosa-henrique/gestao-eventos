using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Application.Eventos.Queries.BuscarEventos;

public record BuscarEventosResult(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima)
{
    public static BuscarEventosResult DoDominio(Evento evento)
    {
        return new BuscarEventosResult(
            evento.Id,
            evento.Detalhes.Nome,
            evento.Detalhes.DataHora,
            evento.Detalhes.Localizacao,
            evento.Detalhes.CapacidadeMaxima);
    }
}