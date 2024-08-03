namespace GestaoEventos.Api.Endpoints.Eventos.BuscarEventos;

public record BuscarEventosResponse(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima, int Status)
{
}