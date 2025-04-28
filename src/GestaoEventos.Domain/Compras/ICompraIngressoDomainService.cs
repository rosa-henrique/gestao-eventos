using System.Collections.Frozen;

using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Compras;

public interface ICompraIngressoDomainService
{
    Task<ErrorOr<CompraIngresso>> RealizarCompra(Evento evento, Guid sessaoId,
        Guid usuarioId, FrozenDictionary<Guid, int> itensCompra, CancellationToken cancellationToken = default);
}