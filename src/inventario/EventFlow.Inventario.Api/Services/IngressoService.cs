using EventFlow.Inventario.Grpc;

using Grpc.Core;

namespace EventFlow.Inventario.Api.Services;

public class IngressoService : Grpc.IngressoService.IngressoServiceBase
{
    public override async Task<ProcessarItensReply> ProcessarItens(ProcessarItensRequest request, ServerCallContext context)
    {
        return await base.ProcessarItens(request, context);
    }
}