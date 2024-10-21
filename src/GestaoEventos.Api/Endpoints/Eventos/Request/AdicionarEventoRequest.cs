namespace GestaoEventos.Api.Endpoints.Eventos.Request;

public record AdicionarEventoRequest(string Nome, DateTime DataHoraInicio, DateTime DataHoraFim, string Localizacao, int CapacidadeMaxima)
{
}