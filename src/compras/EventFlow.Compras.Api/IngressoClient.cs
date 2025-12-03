using EventFlow.Inventario.Grpc;

namespace EventFlow.Compras.Api;

public class IngressoClient(Ingresso.IngressoClient client)
{
    public async Task Test()
    {
        var request = new ProcessarItensRequest();
        await client.ProcessarItensAsync(request);
    }
}