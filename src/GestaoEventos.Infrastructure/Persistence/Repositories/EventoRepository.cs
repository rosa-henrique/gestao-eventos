using GestaoEventos.Domain.Eventos;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public class EventoRepository(AppDbContext context) : Repository<Evento>(context), IEventoRepository
{
    public async Task<Evento?> ObterPorNomeId(string nome, Guid? id = null)
    {
        IList<StatusEvento> statusConsultar = [StatusEvento.Pendente, StatusEvento.Confirmado, StatusEvento.EmAndamento];
        return await _dbSet.FirstOrDefaultAsync(e =>
                                        e.Detalhes.Nome.Equals(nome) &&
                                        statusConsultar.Contains(e.Detalhes.Status) &&
                                        (!id.HasValue || e.Id.Equals(id)));
    }

    public async Task<IEnumerable<Evento>> Buscar()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Evento?> BuscarPorId(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
}