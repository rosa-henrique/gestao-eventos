using GestaoEventos.Domain.Eventos;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public class EventoRepository(AppDbContext context) : Repository<Evento>(context), IEventoRepository
{
    public async Task<Evento?> ObterPorNomeId(string nome, Guid? id = null, CancellationToken cancellationToken = default)
    {
        IList<StatusEvento> statusConsultar = [StatusEvento.Pendente, StatusEvento.Confirmado, StatusEvento.EmAndamento];
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(
            e =>
                e.Nome.Equals(nome) &&
                statusConsultar.Contains(e.Status) &&
                (!id.HasValue || e.Id != id),
            cancellationToken);
    }

    public async Task<IEnumerable<Evento>> Buscar(CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Evento?> BuscarPorId(Guid id, CancellationToken cancellationToken, bool track = true)
    {
        return await _dbSet.AsTracking(track ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking)
                        .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
}