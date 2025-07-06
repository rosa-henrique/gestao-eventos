// Stryker disable all

using System.Reflection;

using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public static class EventoFactory
{
    public static ErrorOr<Evento> CriarEventoComValidacao(
        string? nome = null,
        DateTime? dataHoraInicio = null,
        DateTime? dataHoraFim = null,
        string? localizacao = null,
        int? capacidadeMaxima = null)
    {
        return Evento.Criar(
            nome ?? "teste",
            dataHoraInicio ?? DateTime.UtcNow.AddDays(7),
            dataHoraFim ?? DateTime.UtcNow.AddDays(7).AddHours(2),
            localizacao ?? "Rua teste",
            capacidadeMaxima ?? Evento.CapacidadeMinima + 1,
            Guid.NewGuid());
    }

    public static Evento CriarEvento(
        string? nome = null,
        DateTime? dataHoraInicio = null,
        DateTime? dataHoraFim = null,
        string? localizacao = null,
        int? capacidadeMaxima = null,
        StatusEvento? status = null,
        Guid? criadoPor = null)
    {
        var eventoType = typeof(Evento);

#pragma warning disable S3011
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [
                typeof(string),
                typeof(DateTime),
                typeof(DateTime),
                typeof(string),
                typeof(int),
                typeof(StatusEvento),
                typeof(Guid),
                typeof(Guid?)
            ],
            null)!;

        return (Evento)constructor.Invoke(
        [
            nome ?? "Evento",
            dataHoraInicio ?? DateTime.UtcNow.AddDays(8),
            dataHoraFim ?? DateTime.UtcNow.AddDays(8).AddHours(12),
            localizacao ?? "Localização",
            capacidadeMaxima ?? 100,
            status ?? StatusEvento.Pendente,
            criadoPor ?? Guid.NewGuid(),
            null
        ]);
    }
}