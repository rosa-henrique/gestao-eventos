using ErrorOr;

using EventFlow.Inventario.Grpc;

using Grpc.Core;

using MediatR;

namespace EventFlow.Inventario.Api.Services;

public class IngressoService(IMediator mediator) : Ingresso.IngressoBase
{
    public override async Task<ProcessarItensReply> ProcessarItens(ProcessarItensRequest request, ServerCallContext context)
    {
        var dadosRequest = request.Itens.ToDictionary(item => Guid.Parse(item.Key), item => item.Value);

        var processarItensRequest = new EventFlow.Inventario.Application.Commands.ProcessarItens.ProcessarItensRequest(dadosRequest);

        var processarItensResponse = await mediator.Send(processarItensRequest, context.CancellationToken);

        if (!processarItensResponse.IsError)
        {
            var response = new ProcessarItensReply();
            response.Itens.AddRange(processarItensResponse.Value.Select(v => new ItemOutput
            {
                Id = v.Id.ToString(),
                Valor = (double)v.ValorUnitario,
                EventoId = v.EventoId.ToString(),
            }));

            return response;
        }

        var error = processarItensResponse.FirstError;

        var status = error.Type switch
        {
            ErrorType.Conflict => StatusCode.AlreadyExists,
            ErrorType.Validation => StatusCode.InvalidArgument,
            ErrorType.NotFound => StatusCode.NotFound,
            ErrorType.Failure => StatusCode.Aborted,
            _ => StatusCode.Unknown,
        };

        var trailers = new Metadata
        {
            { "error-code", error.Code },
        };

        throw new RpcException(new Status(status, error.Description), trailers);
    }
}