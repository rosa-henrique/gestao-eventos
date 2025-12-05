using EventFlow.Compras.Domain;

namespace EventFlow.Compras.Infrastructure.Persistence.Repositories;

public class CompraIngressoRepository(ComprasDbContext dbContext) : ICompraIngressoRepository
{
    public void Adicionar(CompraIngresso compraIngresso)
    {
        dbContext.CompraIngressos.Add(compraIngresso);
    }

    public Task<int> SalvarAlteracoes(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}