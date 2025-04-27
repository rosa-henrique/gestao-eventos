namespace GestaoEventos.Domain.Compras;

public static class ErrosCompras
{
    public const string IngressoNaoEncontrado = "Ingresso {0} não encontrado";
    public const string QuantidadeIngressoIndisponivel = "Quantidade informada excede limite de ingressos disponiveis";
    public const string SessaoNaoEncontrada = "Sessão não encontrada";
    public const string EventoNaoPermiteCompra = "Não é possível comprar ingressos para esse evento";
}