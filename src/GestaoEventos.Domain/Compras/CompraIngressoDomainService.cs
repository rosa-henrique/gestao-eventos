using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Compras;

public class CompraIngressoDomainService(ICompraIngressoRepository compraIngressoRepository)
    : ICompraIngressoDomainService
{
    public async Task<ErrorOr<CompraIngresso>> RealizarCompra(Evento evento, Guid sessaoId,
        Guid usuarioId, IDictionary<Guid, int> itensCompra, CancellationToken cancellationToken = default)
    {
        if (!evento.EventoPermiteCompraIngresso())
        {
            return Error.Failure(description: ErrosCompras.EventoNaoPermiteCompra);
        }

        var ingressosDisponiveis = evento.Ingressos.ToDictionary(i => i.Id, i => i);
        var compraIngressosPorSessao =
            await compraIngressoRepository.ObterQuantidadeIngressosVendidosPorSessao(sessaoId, cancellationToken);
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

        var compraIngresso = new CompraIngresso(usuarioId, sessaoId, ingressosComprados);
        compraIngresso.IngressosComprado(evento);

        return compraIngresso;
    }
}