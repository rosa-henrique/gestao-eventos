using System.Reflection;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public class TipoIngressoFactory
{
    public static TipoIngresso CriarTipoIngresso(string? nome = null, string? descricao = null)
    {
        var eventoType = typeof(TipoIngresso);
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [typeof(string), typeof(string)],
            null)!;

        return (TipoIngresso)constructor.Invoke([
            nome ?? "Evento",
            descricao ?? "Descrição"
        ]);
    }
}