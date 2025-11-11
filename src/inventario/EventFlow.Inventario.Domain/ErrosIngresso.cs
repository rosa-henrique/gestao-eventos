using ErrorOr;

namespace EventFlow.Inventario.Domain;

public static class ErrosIngresso
{
    public const string StatusEventoNaoPermiteAlteracaoIngressoCode = "Ingresso.StatusEventoNaoPermiteAlteracaoIngresso";
    public const string EventoNaoEncontradoCode = "Ingresso.EventoNaoEncontrado";

    public static Error StatusEventoNaoPermiteAlteracaoIngresso(StatusEvento statusEvento) => Error.Failure(
        code: StatusEventoNaoPermiteAlteracaoIngressoCode,
        description: $"Evento relacionado está com status {statusEvento.NomeExibicao} e não permite inclusão/alteração de ingresso");

    public static Error EventoNaoEncontrado = Error.NotFound(
        code: EventoNaoEncontradoCode,
        description: "Eventos selecionado não encontrado.");
}