using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public static class EventoFactory
{
    public static ErrorOr<Evento> CriarEvento(
        string? nome = null,
        DateTime? dataHora = null,
        string? localizacao = null,
        int? capacidadeMaxima = null)
    {
        return Evento.Criar(
            nome ?? "teste",
            dataHora ?? DateTime.Now.AddDays(7),
            localizacao ?? "Rua teste",
            capacidadeMaxima ?? DetalhesEvento.CapacidadeMinima + 1);
    }
}