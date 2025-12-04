using EventFlow.Compras.Domain;
using EventFlow.Inventario.Grpc;

namespace EventFlow.Compras.Infrastructure.Clients;

public class IngressoClient(Ingresso.IngressoClient client) : IIngressoClient
{
    public async Task ProcessarItens(IDictionary<string, int> dadosRequest)
    {
        var request = new ProcessarItensRequest();
        foreach (var item in dadosRequest)
        {
            request.Itens.Add(item.Key, item.Value);
        }

        await client.ProcessarItensAsync(request);
    }
}