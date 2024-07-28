using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public interface IEventoRepository : IRepository<Evento>
{
    Task<Evento?> ObterPorNomeId(string nome, Guid? id = null);
}