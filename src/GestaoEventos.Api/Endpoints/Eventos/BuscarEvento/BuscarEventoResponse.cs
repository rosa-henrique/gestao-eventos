namespace GestaoEventos.Api.Endpoints.Eventos.BuscarEvento;

public record BuscarEventoResponse(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima, int Status)
{
}