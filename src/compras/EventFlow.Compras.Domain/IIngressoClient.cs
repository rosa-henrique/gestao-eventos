namespace EventFlow.Compras.Domain;

public interface IIngressoClient
{
    Task ProcessarItens(IDictionary<string, int> dadosRequest);
}