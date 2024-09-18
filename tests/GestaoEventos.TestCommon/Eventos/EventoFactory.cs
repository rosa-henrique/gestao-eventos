﻿// Stryker disable all
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
            [typeof(DetalhesEvento), typeof(Guid?)],
            null)!;

        var detalhesEvento = DetalheEventoFactory.CriarDetalheEvento(nome, dataHora, localizacao, capacidadeMaxima, status);

        return (Evento)constructor.Invoke([
            detalhesEvento,
            id]);
    }
}