// Stryker disable all
using System.Reflection;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public class DetalheEventoFactory
{
    public static DetalhesEvento CriarDetalheEvento(
        string? nome = null,
        DateTime? dataHoraInicio = null,
        DateTime? dataHoraFim = null,
        string? localizacao = null,
        int? capacidadeMaxima = null,
        StatusEvento? status = null)
    {
        var eventoType = typeof(DetalhesEvento);
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [typeof(string), typeof(DateTime), typeof(DateTime), typeof(string), typeof(int), typeof(StatusEvento)],
            null)!;

        return (DetalhesEvento)constructor.Invoke([
            nome ?? "Evento",
            dataHoraInicio ?? DateTime.UtcNow.AddDays(8),
            dataHoraFim ?? DateTime.UtcNow.AddDays(8).AddHours(12),
            localizacao ?? "Localização",
            capacidadeMaxima ?? 100,
            status ?? StatusEvento.Pendente]);
    }
}