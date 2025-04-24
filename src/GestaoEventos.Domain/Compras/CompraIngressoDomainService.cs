using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Compras;

public class CompraIngressoDomainService(ICompraIngressoRepository compraIngressoRepository)
    : ICompraIngressoDomainService
{
    public async Task<ErrorOr<CompraIngresso>> RealizarCompra(IEnumerable<Ingresso> ingressos, Guid sessaoId,
        Guid usuarioId, IDictionary<Guid, int> itensCompra, CancellationToken cancellationToken)
    {
        var ingressosDisponiveis = ingressos.ToDictionary(i => i.Id, i => i);
        var compraIngressosPorSessao =
            await compraIngressoRepository.ObterQuantidadeIngressosVendidosPorSessao(sessaoId);
        IList<IngressoComprado> ingressosComprados = new List<IngressoComprado>();

        foreach (var itemCompra in itensCompra)
        {
            if (!ingressosDisponiveis.TryGetValue(itemCompra.Key, out var ingressoDisponivel))
            {
                return Error.NotFound(description: string.Format(ErrosCompras.IngressoNaoEncontrado, itemCompra.Key));
            }

            if (itemCompra.Value > ingressoDisponivel.Quantidade)
            {
                return Error.Failure(description: ErrosCompras.QuantidadeIngressoIndisponivel);
            }

            if (compraIngressosPorSessao.TryGetValue(itemCompra.Key, out var compraIngressoPorSessao) &&
                (compraIngressoPorSessao + itemCompra.Value) > ingressoDisponivel.Quantidade)
            {
                return Error.Failure(description: ErrosCompras.QuantidadeIngressoIndisponivel);
            }

            ingressosComprados.Add(new IngressoComprado(itemCompra.Key, itemCompra.Value,
                ingressoDisponivel.Preco));
        }

        return new CompraIngresso(usuarioId, sessaoId, ingressosComprados);
    }
}