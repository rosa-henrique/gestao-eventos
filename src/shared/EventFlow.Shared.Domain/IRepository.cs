namespace EventFlow.Shared.Domain;

public interface IRepository<TAggregate>
    where TAggregate : IAggregateRoot;