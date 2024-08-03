using System.Reflection;

using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public static class EventoFactory
{
    public static ErrorOr<Evento> CriarEventoComValidacao(
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

    public static Evento CriarEvento(
        string? nome = null,
        DateTime? dataHora = null,
        string? localizacao = null,
        int? capacidadeMaxima = null,
        StatusEvento? status = null,
        Guid? id = null)
    {
        var eventoType = typeof(Evento);
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [typeof(string), typeof(DateTime), typeof(string), typeof(int), typeof(StatusEvento), typeof(Guid?)],
            null)!;

        return (Evento)constructor.Invoke([
            nome ?? "Evento",
            dataHora ?? DateTime.UtcNow.AddDays(8),
            localizacao ?? "Localização",
            capacidadeMaxima ?? 100,
            status ?? StatusEvento.Pendente,
            id]);
    }
}