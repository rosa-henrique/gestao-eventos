using GestaoEventos.Domain.Eventos;

using Microsoft.EntityFrameworkCore;

namespace GestaoEventos.Infrastructure.Persistence.Repositories;

public class EventoRepository(AppDbContext context) : Repository<Evento>(context), IEventoRepository
{
    public async Task<Evento?> ObterPorNomeId(string nome, Guid? id = null)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Detalhes.Nome.Equals(nome));
    }
}