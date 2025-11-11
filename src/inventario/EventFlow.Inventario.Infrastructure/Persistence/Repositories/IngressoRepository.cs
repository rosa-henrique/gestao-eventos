using EventFlow.Inventario.Domain;

using Microsoft.EntityFrameworkCore;

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

    public Task<int> SalvarAlteracoes(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}