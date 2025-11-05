using EventFlow.Eventos.Domain;

namespace EventFlow.Eventos.Infrastructure.Persistence;

public class EventoRepository(EventosDbContext dbContext) : IEventoRepository
{
    public void Adicionar(Evento evento)
    {
        dbContext.Eventos.Add(evento);
    }

    public Task<int> SalvarAlteracoes(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}