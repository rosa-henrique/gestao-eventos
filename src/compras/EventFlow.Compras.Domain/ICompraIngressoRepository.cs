namespace EventFlow.Compras.Domain;

public interface ICompraIngressoRepository
{
    void Adicionar(CompraIngresso compraIngresso);
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken);
}