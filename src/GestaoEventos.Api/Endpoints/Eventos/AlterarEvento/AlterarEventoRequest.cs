namespace GestaoEventos.Api.Endpoints.Eventos.AlterarEvento;

public record AlterarEventoRequest(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima, int Status)
{
}