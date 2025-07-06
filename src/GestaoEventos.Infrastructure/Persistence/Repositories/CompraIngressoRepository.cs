using GestaoEventos.Domain.Compras;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public class CompraIngressoRepository(AppDbContext context)
    : Repository<CompraIngresso>(context), ICompraIngressoRepository
{
    public async Task<Dictionary<Guid, int>> ObterQuantidadeIngressosVendidosPorSessao(Guid sessaoId,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(cp => cp.SessaoId == sessaoId)
            .SelectMany(cp => cp.Ingressos) // Achata a lista de ingressos
            .GroupBy(i => i.IngressoId)
            .Select(g => new { IngressoId = g.Key, QuantidadeTotal = g.Sum(i => i.Quantidade) })
            .ToDictionaryAsync(x => x.IngressoId, x => x.QuantidadeTotal, cancellationToken);
    }
}