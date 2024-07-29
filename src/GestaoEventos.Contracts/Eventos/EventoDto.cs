namespace GestaoEventos.Contracts.Eventos;

public record EventoDto(Guid Id, string Nome, DateTime DataHora, string Localizacao, int CapacidadeMaxima)
{
}