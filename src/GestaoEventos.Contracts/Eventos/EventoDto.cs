namespace GestaoEventos.Contracts.Eventos;

public class EventoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public DateTime DataHora { get; set; }
    public string Localizacao { get; set; } = null!;
    public int CapacidadeMaxima { get; set; }
}