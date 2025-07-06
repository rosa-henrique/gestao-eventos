using System.Collections.Frozen;

using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Compras;

public class CompraIngressoDomainService(ICompraIngressoRepository compraIngressoRepository)
    : ICompraIngressoDomainService
{
    public async Task<ErrorOr<CompraIngresso>> RealizarCompra(Evento evento, Guid sessaoId,
        Guid usuarioId, FrozenDictionary<Guid, int> itensCompra, CancellationToken cancellationToken = default)
    {
        if (!evento.EventoPermiteCompraIngresso())
        {
            return Error.Failure(description: ErrosCompras.EventoNaoPermiteCompra);
        }

        var ingressosDisponiveis = evento.Ingressos.ToFrozenDictionary(i => i.Id, i => i);
        var compraIngressosPorSessao =
            await compraIngressoRepository.ObterQuantidadeIngressosVendidosPorSessao(sessaoId, cancellationToken);
        var ingressosComprados = new List<IngressoComprado>();

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