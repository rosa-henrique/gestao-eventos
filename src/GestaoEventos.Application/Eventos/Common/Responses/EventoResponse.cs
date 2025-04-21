namespace GestaoEventos.Application.Eventos.Common.Responses;

public record EventoResponse(
    Guid Id,
    string Nome,
    DateTime DataHoraInicio,
    DateTime DataHoraFim,
    string Localizacao,
    int CapacidadeMaxima,
    string Status) : BaseEventoResponse(Id, Nome, DataHoraInicio, DataHoraFim, Localizacao, CapacidadeMaxima, Status)
{
}