using ErrorOr;

using GestaoEventos.Domain.Eventos;

namespace GestaoEventos.Domain.Compras;

public interface ICompraIngressoDomainService
{
    Task<ErrorOr<CompraIngresso>> RealizarCompra(Evento evento, Guid sessaoId,
        Guid usuarioId, IDictionary<Guid, int> itensCompra, CancellationToken cancellationToken = default);
}