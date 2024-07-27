namespace GestaoEventos.Domain.Eventos;

public class ErrosEvento
{
    public const string DataRetroativa = "A data do evento deve ser no futuro.";
    public const string CapacidadeInvalida = "A capacidade máxima deve ser um número positivo.";
}