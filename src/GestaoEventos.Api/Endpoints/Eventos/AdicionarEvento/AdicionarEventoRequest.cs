namespace GestaoEventos.Api.Endpoints.Eventos.AdicionarEvento;

public record AdicionarEventoRequest(string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima)
{
}