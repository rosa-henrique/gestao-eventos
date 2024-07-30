namespace GestaoEventos.Domain.Eventos;

public class ErrosEvento
{
    public const string DataRetroativa = "A data do evento deve ser no futuro.";
    public const string CapacidadeInvalida = "A capacidade máxima deve ser um número positivo.";
    public const string NomeEventoJaExiste = "Já existe um evento com este nome.";
    public const string EventoNaoEncontrado = "Evento não encontrado.";
    public const string NaoAlterarEventoPassado = "Eventos passados não podem ser alterados.";
}