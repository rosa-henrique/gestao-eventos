namespace EventFlow.Inventario.Domain;

public interface IIngressoRepository
{
    Task<Ingresso?> BuscarPorId(Guid id, CancellationToken cancellationToken);
    Task<List<Ingresso>> BuscarPorIds(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<IList<Ingresso>> BuscarPorEvento(Guid eventoId, CancellationToken cancellationToken);
    void Adicionar(Ingresso ingresso);
    void Alterar(Ingresso ingresso);
    void Alterar(IEnumerable<Ingresso> ingressos);
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken);
}