using EventFlow.Inventario.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventFlow.Inventario.Infrastructure.Persistence.Repositories;

public class IngressoRepository(InventarioDbContext dbContext) : IIngressoRepository
{
    public Task<Ingresso?> BuscarPorId(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Ingressos
            .Include(i => i.Evento)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<List<Ingresso>> BuscarPorIds(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        return dbContext.Ingressos
            .Include(i => i.Evento)
            .AsNoTracking()
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Ingresso>> BuscarPorEvento(Guid eventoId, CancellationToken cancellationToken)
    {
        return await dbContext.Ingressos.Where(i => i.EventoId == eventoId).ToListAsync(cancellationToken: cancellationToken);
    }

    public void Adicionar(Ingresso ingresso)
    {
        dbContext.Ingressos.Add(ingresso);
    }

    public void Alterar(Ingresso ingresso)
    {
        dbContext.Ingressos.Update(ingresso);
    }

    public void Alterar(IEnumerable<Ingresso> ingressos)
    {
        dbContext.Ingressos.UpdateRange(ingressos);
    }

    public Task<int> SalvarAlteracoes(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}