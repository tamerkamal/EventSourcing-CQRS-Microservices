namespace CQRS.Core.Infrastructure;

using CQRS.Core.Events;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId,
                         IEnumerable<BaseEvent> events,
                         int expectedVersion);

    Task<List<BaseEvent>> GetEventsOrderedDescendingByVersionAsync(Guid aggregateId);
}
