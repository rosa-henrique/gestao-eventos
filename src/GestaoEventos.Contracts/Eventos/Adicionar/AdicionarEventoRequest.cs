namespace GestaoEventos.Contracts.Eventos.Adicionar;

public record AdicionarEventoRequest(string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima)
{
}