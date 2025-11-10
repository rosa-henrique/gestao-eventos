namespace EventFlow.Inventario.Domain;

public interface IEventoRepository
{
    Task<Evento?> BuscarPorId(Guid id, CancellationToken cancellationToken);
    void Adicionar(Evento evento);
    void Alterar(Evento evento);
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken);
}