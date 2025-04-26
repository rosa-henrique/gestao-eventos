// Stryker disable all

using System.Reflection;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public class IngressoFactory
{
    public static Ingresso CriarIngresso(
        string? nome = null,
        string? descricao = null,
        decimal? preco = null,
        int? quantidade = null,
        Guid? id = null)
    {
        var eventoType = typeof(Ingresso);
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [typeof(string), typeof(string), typeof(decimal), typeof(int), typeof(Guid?)],
            null)!;

        return (Ingresso)constructor.Invoke([
            nome ?? "Evento",
            descricao ?? "Descricao evento",
            preco ?? 100,
            quantidade ?? 100,
            id ?? Guid.NewGuid()
        ]);
    }
}