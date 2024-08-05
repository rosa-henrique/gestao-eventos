namespace GestaoEventos.Domain.Eventos;

public class ErrosEvento
{
    public const string DataRetroativa = "A data do evento deve ser no futuro.";
    public const string CapacidadeInvalida = "A capacidade máxima deve ser um número positivo.";
    public const string NomeEventoJaExiste = "Já existe um evento com este nome.";
    public const string EventoNaoEncontrado = "Evento não encontrado.";
    public const string NaoAlterarEventoPassado = "Eventos passados não podem ser alterados.";
    public const string NomeIngressoJaExiste = "Nome do ingresso já existe dentro do contexto do evento.";
    public const string NaoPermiteAlteracao = "Não é possível alterar o status de um evento {0}.";
    public const string NaoPermiteAlteracaoDiretamente = "Não é possível alterar um evento {0} diretamente para {1}.";
    public const string NaoPermiteAdicaoIngresso = "Não é possível adicionar ingressos para um evento {0}.";
    public const string QuantidadeTotalIngressosExcedeCapacidadeMaxima = "Quantidade total de ingressos excede a capacidade máxima do evento.";
}