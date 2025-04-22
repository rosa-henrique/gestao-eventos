namespace GestaoEventos.Domain.Eventos;

public class ErrosEvento
{
    public const string DataRetroativa = "A data do evento deve ser no futuro.";
    public const string DataFinalMenorIgualFinal = "A data de fim do evento deve ser após a de início.";
    public const string CapacidadeInvalida = "A capacidade máxima deve ser um número positivo.";
    public const string NomeEventoJaExiste = "Já existe um evento com este nome.";
    public const string EventoNaoEncontrado = "Evento não encontrado.";
    public const string NaoAlterarEventoPassado = "Eventos passados não podem ser alterados.";
    public const string NomeIngressoJaExiste = "Nome do ingresso já existe dentro do contexto do evento.";
    public const string NaoPermiteAlteracao = "Não é possível alterar o status de um evento {0}.";
    public const string NaoPermiteAlteracaoDiretamente = "Não é possível alterar um evento {0} diretamente para {1}.";
    public const string NaoPermiteAdicaoIngresso = "Não é possível adicionar ingressos para um evento {0}.";
    public const string QuantidadeTotalIngressosExcedeCapacidadeMaxima = "Quantidade total de ingressos excede a capacidade máxima do evento.";
    public const string SessaoDataFinalMenorIgualFinal = "A data de fim da sessão deve ser após a de início.";
    public const string ConflitoDataHoraSessao = "Há um conflito de horário com outra sessão.";
    public const string DataSessaoForaIntervaloEvento = "Data da sessão não pode estar fora do intervalo do evento.";
    public const string ConflitoSessoes = "As novas datas do evento entram em conflito com uma ou mais sessões associadas. Altere as datas das sessões ou ajuste as datas do evento.";
    public const string SessaoNaoEncontrada = "Sessao não encontrada.";
    public const string UsuarioNaoAutorizado = "Usuário não autorizado para realizar essa operação.";
}