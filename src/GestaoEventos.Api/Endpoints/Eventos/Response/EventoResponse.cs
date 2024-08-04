namespace GestaoEventos.Api.Endpoints.Eventos.Response;

public record EventoResponse(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima, int Status)
{
}