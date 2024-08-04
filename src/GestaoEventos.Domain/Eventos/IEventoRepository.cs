using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Eventos;

public interface IEventoRepository : IRepository<Evento>
{
    Task<Evento?> ObterPorNomeId(string nome, Guid? id = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Evento>> Buscar(CancellationToken cancellationToken);
    Task<Evento?> BuscarPorId(Guid id, CancellationToken cancellationToken, bool track = true);
}