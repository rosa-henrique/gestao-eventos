// Stryker disable all
using System.Reflection;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.TestCommon.Eventos;

public static class SessaoFactory
{
    public static Sessao CriarSessao(
       string? nome = null,
       DateTime? dataHoraInicio = null,
       DateTime? dataHoraFim = null,
       Guid? id = null)
    {
        var eventoType = typeof(Sessao);
        var constructor = eventoType.GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [typeof(string), typeof(DateTime), typeof(DateTime), typeof(Guid?)],
            null)!;

        return (Sessao)constructor.Invoke([nome, dataHoraInicio, dataHoraFim, id]);
    }
}