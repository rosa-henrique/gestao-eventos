namespace GestaoEventos.Api.Endpoints.Eventos.Request;

public record AdicionarEventoRequest(string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima)
{
}
