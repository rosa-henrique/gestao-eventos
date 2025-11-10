using EventFlow.Inventario.Domain;

using Microsoft.EntityFrameworkCore;

namespace EventFlow.Inventario.Infrastructure.Persistence.Repositories;

public class EventoRepository(InventarioDbContext dbContext) : IEventoRepository
{
    public Task<Evento?> BuscarPorId(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Eventos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Adicionar(Evento evento)
    {
        dbContext.Eventos.Add(evento);
    }

    public void Alterar(Evento evento)
    {
        dbContext.Eventos.Update(evento);
    }

    public Task<int> SalvarAlteracoes(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}