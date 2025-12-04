using ErrorOr;

using EventFlow.Compras.Domain;

using MediatR;

namespace EventFlow.Compras.Application.Commands.ComprarIngressos;

public class ComprarIngressosRequestHandler(IIngressoClient ingressoClient) : IRequestHandler<ComprarIngressosRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(ComprarIngressosRequest request, CancellationToken cancellationToken)
    {
        var processarItensRequest = request.IngressosCompra.ToDictionary(e => e.Key.ToString(), e => e.Value);
        await ingressoClient.ProcessarItens(processarItensRequest);
        throw new NotImplementedException();
    }
}