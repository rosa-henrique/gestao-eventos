// Stryker disable all
using System.Reflection;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public class IngressoFactory
{
    public static Ingresso CriarDetalheEvento(
       string? nome = null,
       DateTime? dataHora = null,
       string? localizacao = null,
       int? capacidadeMaxima = null,
       StatusEvento? status = null)
    {
        var eventoType = typeof(Ingresso);
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [typeof(string), typeof(string), typeof(decimal), typeof(int), typeof(Guid?)],
            null)!;

        return (Ingresso)constructor.Invoke([
            nome ?? "Evento",
            dataHora ?? DateTime.UtcNow.AddDays(8),
            localizacao ?? "Localização",
            capacidadeMaxima ?? 100,
            status ?? StatusEvento.Pendente]);
    }
}