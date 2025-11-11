namespace EventFlow.Inventario.Domain;

public interface IIngressoRepository
{
    Task<Ingresso?> BuscarPorId(Guid id, CancellationToken cancellationToken);
    Task<IList<Ingresso>> BuscarPorEvento(Guid eventoId, CancellationToken cancellationToken);
    void Adicionar(Ingresso ingresso);
    void Alterar(Ingresso ingresso);
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken);
}