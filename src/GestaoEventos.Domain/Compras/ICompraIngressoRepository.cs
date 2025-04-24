using GestaoEventos.Domain.Common;

namespace GestaoEventos.Domain.Compras;

public interface ICompraIngressoRepository : IRepository<CompraIngresso>
{
    Task<Dictionary<Guid, int>> ObterQuantidadeIngressosVendidosPorSessao(Guid sessaoId);
}