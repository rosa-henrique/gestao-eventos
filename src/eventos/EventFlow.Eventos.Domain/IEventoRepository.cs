using EventFlow.Shared.Domain;

namespace EventFlow.Eventos.Domain;

public interface IEventoRepository : IRepository<Evento>
{
    void Adicionar(Evento evento);
    Task<int> SalvarAlteracoes(CancellationToken cancellationToken);
}