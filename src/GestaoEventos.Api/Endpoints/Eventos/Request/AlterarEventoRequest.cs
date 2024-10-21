namespace GestaoEventos.Api.Endpoints.Eventos.Request;

public record AlterarEventoRequest(string Nome, DateTime DataHoraInicio, DateTime DataHoraFim, string Localizacao, int CapacidadeMaxima, int Status)
{
}