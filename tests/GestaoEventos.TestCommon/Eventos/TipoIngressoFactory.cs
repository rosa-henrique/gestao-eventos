using System.Reflection;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public static class TipoIngressoFactory
{
    public static TipoIngresso CriarTipoIngresso(string? nome = null, string? descricao = null)
    {
        var eventoType = typeof(TipoIngresso);

#pragma warning disable S3011
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