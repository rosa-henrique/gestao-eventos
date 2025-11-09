using ErrorOr;

namespace EventFlow.Inventario.Domain;

public static class ErrosIngresso
{
    public const string EventoNaoPermiteCompraIngressoCode = "Ingresso.EventoNaoPermiteCompraIngresso";

    public static Error EventoNaoPermiteCompraIngresso = Error.Failure(
        code: EventoNaoPermiteCompraIngressoCode,
        description: "A data do evento deve ser no futuro.");
}