namespace EventFlow.Shared.Application.Contracts;

public record EventoContract(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    StatusEvento Status,
    Guid CriadoPor) : IContract;

public enum StatusEvento
{
    Pendente,
    Confirmado,
    Cancelado,
    EmAndamento,
    Concluído,
}