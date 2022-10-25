namespace CQRS.Core.Infrastructure;

using CQRS.Core.Events;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId,
                         List<BaseEvent> events,
                         int expectedVersion);

    Task<List<BaseEvent>> GetEventsOrderedByVersionAsync(Guid aggregateId);
    Task<List<Guid>> GetAggregateIdsAsync();
}
