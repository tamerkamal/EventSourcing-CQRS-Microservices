namespace CQRS.Core.Handlers;

using CQRS.Core.Domain;

public interface IEventSourcingHandler<TAggregate>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<TAggregate> GetByIdAsync(Guid id);
    Task RepublishEventsAsync();
}
