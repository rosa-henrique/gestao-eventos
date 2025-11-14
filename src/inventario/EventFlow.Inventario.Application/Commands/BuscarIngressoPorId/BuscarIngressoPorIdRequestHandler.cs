using ErrorOr;

using EventFlow.Inventario.Application.Response;
using EventFlow.Inventario.Domain;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.BuscarIngressoPorId;

public class BuscarIngressoPorIdRequestHandler(IIngressoRepository ingressoRepository) : IRequestHandler<BuscarIngressoPorIdRequest, ErrorOr<IngressoResponse>>
{
    public async Task<ErrorOr<IngressoResponse>> Handle(BuscarIngressoPorIdRequest request, CancellationToken cancellationToken)
    {
        var ingresso = await ingressoRepository.BuscarPorId(request.Id, cancellationToken);

        return ingresso is null ? ErrosIngresso.IngressoNaoEncontrado : (IngressoResponse)ingresso;
    }
}