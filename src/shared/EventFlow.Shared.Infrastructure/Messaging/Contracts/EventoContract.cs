namespace EventFlow.Shared.Infrastructure.Messaging.Contracts;

public record EventoContract(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    StatusEvento StatusEvento) : IContract;

public enum StatusEvento
{
    Pendente,
    Confirmado,
    Cancelado,
    EmAndamento,
    Concluído,
}