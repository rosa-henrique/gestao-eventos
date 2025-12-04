using ErrorOr;

namespace EventFlow.Inventario.Domain;

public static class ErrosIngresso
{
    public const string StatusEventoNaoPermiteAlteracaoIngressoCode = "Ingresso.StatusEventoNaoPermiteAlteracaoIngresso";
    public const string EventoNaoEncontradoCode = "Ingresso.EventoNaoEncontrado";
    public const string IngressoNaoEncontradoCode = "Ingresso.IngressoNaoEncontrado";
    public const string QuantidadeIngressoInsuficienteCode = "Ingresso.QuantidadeIngressoInsuficiente";
    public const string IngressosInformadosParaReservarInvalidosCode = "Ingresso.IngressosInformadosParaReservarInvalidos";
    public const string NaoFoiPossivelReservarIngressosCode = "Ingresso.NaoFoiPossivelReservarIngressos";

    public static Error StatusEventoNaoPermiteAlteracaoIngresso(StatusEvento statusEvento) => Error.Failure(
        code: StatusEventoNaoPermiteAlteracaoIngressoCode,
        description: $"Evento relacionado está com status {statusEvento.NomeExibicao} e não permite inclusão/alteração de ingresso");

    public static Error EventoNaoEncontrado = Error.NotFound(
        code: EventoNaoEncontradoCode,
        description: "Evento selecionado não encontrado.");

    public static Error IngressoNaoEncontrado = Error.NotFound(
        code: IngressoNaoEncontradoCode,
        description: "Ingresso selecionado não encontrado.");

    public static Error QuantidadeIngressoInsuficiente = Error.Failure(
        code: QuantidadeIngressoInsuficienteCode,
        description: "Quantidade de ingressos disponível é insuficiente.");

    public static Error IngressosInformadosParaReservarInvalidos = Error.Failure(
        code: IngressosInformadosParaReservarInvalidosCode,
        description: "Ingressos informados para compra são inválidos.");

    public static Error NaoFoiPossivelReservarIngressos = Error.Conflict(
        code: NaoFoiPossivelReservarIngressosCode,
        description: "Não foi possível reservar ingressos.");
}