namespace GestaoEventos.Api.Endpoints.Eventos.Request;

public record AlterarEventoRequest(string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima, int Status)
{
}