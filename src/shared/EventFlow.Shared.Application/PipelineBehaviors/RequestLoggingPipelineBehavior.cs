using ErrorOr;

using MediatR;

using Microsoft.Extensions.Logging;

using Serilog.Context;

namespace EventFlow.Shared.Application.PipelineBehaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation(
            "Processanto request {RequestName}", requestName);

        TResponse result = await next();

        if (!result.IsError)
        {
            logger.LogInformation(
                "Request {RequestName} concluído", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Errors, true))
            {
                logger.LogError(
                    "Request {RequestName} concluído com erro", requestName);
            }
        }

        return result;
    }
}