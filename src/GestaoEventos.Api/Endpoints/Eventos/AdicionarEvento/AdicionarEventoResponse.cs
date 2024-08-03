namespace GestaoEventos.Api.Endpoints.Eventos.AdicionarEvento;

public record AdicionarEventoResponse(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima, int Status)
{
}