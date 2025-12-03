using ErrorOr;

using MediatR;

namespace EventFlow.Compras.Application.Commands.ComprarIngressos;

public class ComprarIngressosRequestHandler : IRequestHandler<ComprarIngressosRequest, ErrorOr<Success>>
{
    public Task<ErrorOr<Success>> Handle(ComprarIngressosRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}