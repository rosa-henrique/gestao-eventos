using ErrorOr;

using EventFlow.Compras.Application.Dtos;
using EventFlow.Compras.Application.Interfaces;
using EventFlow.Inventario.Grpc;

using Grpc.Core;

namespace EventFlow.Compras.Infrastructure.Clients;

public class IngressoClient(Ingresso.IngressoClient client) : IIngressoClient
{
    public async Task<ErrorOr<IEnumerable<ProcessarItensResponseDto>>> ProcessarItens(IDictionary<string, int> dadosRequest)
    {
        var request = new ProcessarItensRequest();
        foreach (var item in dadosRequest)
        {
            request.Itens.Add(item.Key, item.Value);
        }

        try
        {
            var response = await client.ProcessarItensAsync(request);

            return response.Itens.Select(i =>
                new ProcessarItensResponseDto(Guid.Parse(i.Id), (decimal)i.Valor, Guid.Parse(i.EventoId)))
                as dynamic;
        }
        catch (RpcException ex)
        {
            var mapped = GrpcToErrorOrMapper.TryMap(ex);

            if (mapped is null)
            {
                throw; // fallback natural
            }

            return mapped.Value;
        }
    }
}

public static class GrpcToErrorOrMapper
{
    private static readonly Dictionary<StatusCode, ErrorType> ErrorTypeMap = new()
    {
        { StatusCode.AlreadyExists, ErrorType.Conflict },
        { StatusCode.InvalidArgument, ErrorType.Validation },
        { StatusCode.NotFound, ErrorType.NotFound },
        { StatusCode.FailedPrecondition, ErrorType.Conflict },
        { StatusCode.Aborted, ErrorType.Failure },
    };

    public static Error? TryMap(RpcException ex)
    {
        if (!ErrorTypeMap.TryGetValue(ex.StatusCode, out var errorType))
        {
            // Não tem mapeamento → cliente deve rethrow
            return null;
        }

        var code = ex.Trailers.GetValue("error-code") ?? "unknown";

        return Error.Custom(
            code: code,
            description: ex.Status.Detail,
            type: (int)errorType);
    }
}
