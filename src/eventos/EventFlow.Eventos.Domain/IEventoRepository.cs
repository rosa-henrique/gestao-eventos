using EventFlow.Shared.Domain;

namespace EventFlow.Eventos.Domain;

public interface IEventoRepository : IRepository<Evento>
{
    Task<Evento?> BuscarPorId(Guid id, CancellationToken cancellationToken);
    void Adicionar(Evento evento);
    void Alterar(Evento evento);
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken);
}