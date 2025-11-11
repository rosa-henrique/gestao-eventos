using ErrorOr;

using EventFlow.Inventario.Application.Response;
using EventFlow.Inventario.Domain;

using MediatR;

namespace EventFlow.Inventario.Application.Commands.CriarIngresso;

public class CriarIngressoRequestHandler(IEventoRepository eventoRepository, IIngressoRepository ingressoRepository) : IRequestHandler<CriarIngressoRequest, ErrorOr<IngressoResponse>>
{
    public async Task<ErrorOr<IngressoResponse>> Handle(CriarIngressoRequest request, CancellationToken cancellationToken)
    {
        var evento = await eventoRepository.BuscarPorId(request.EventoId, cancellationToken);
        if (evento is null)
        {
            return ErrosIngresso.EventoNaoEncontrado;
        }

        var criarIngressoResult = Ingresso.Criar(request.Nome,  request.Descricao, request.Preco, request.QuantidadeTotal, evento);
        if (criarIngressoResult.IsError)
        {
            return criarIngressoResult.Errors;
        }

        var ingresso = criarIngressoResult.Value;

        ingressoRepository.Adicionar(ingresso);
        await ingressoRepository.SalvarAlteracoes(cancellationToken);

        return (IngressoResponse)ingresso;
    }
}