using ErrorOr;

namespace EventFlow.Eventos.Domain;

public static class ErrosEvento
{
    public const string DataRetroativaCode = "Evento.DataRetroativa";
    public const string DataFinalMenorIgualFinalCode = "Evento.DataFinalMenorIgualFinal";
    public const string CapacidadeExcedidaCode = "Evento.CapacidadeExcedida";
    public const string NaoAlterarEventoPassadoCode = "Evento.NaoAlterarEventoPassado";
    public const string NaoPermiteAlteracaoCode = "Evento.NaoPermiteAlteracao";
    public const string NaoPermiteAlteracaoDiretamenteCode = "Evento.NaoPermiteAlteracaoDiretamente";

    public static Error DataRetroativa = Error.Failure(
        code: DataRetroativaCode,
        description: "A data do evento deve ser no futuro.");
    public static Error DataFinalMenorIgualFinal = Error.Failure(
        code: DataFinalMenorIgualFinalCode,
        description: "A data de fim do evento deve ser após a de início.");
    public static Error CapacidadeInvalida = Error.Failure(
        code: CapacidadeExcedidaCode,
        description: "A capacidade máxima deve ser um número positivo.");
    public static Error NaoAlterarEventoPassado = Error.Failure(
        code: NaoAlterarEventoPassadoCode,
        description: "Eventos passados não podem ser alterados.");
    public static Error NaoPermiteAlteracao(StatusEvento status) => Error.Failure(
        code: NaoPermiteAlteracaoCode,
        description: $"Não é possível alterar o status de um evento {status}.");
    public static Error NaoPermiteAlteracaoDiretamente(StatusEvento statusAtual, StatusEvento statusNovo) => Error.Failure(
        code: NaoPermiteAlteracaoDiretamenteCode,
        description: $"Não é possível alterar um evento {statusAtual} diretamente para {statusNovo}.");
}