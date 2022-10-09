namespace CQRS.Core.Handlers;

using CQRS.Core.Domain;

public interface IEventSourcingHandler<T>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid id);
}
