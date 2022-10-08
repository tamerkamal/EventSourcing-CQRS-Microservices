namespace CQRS.Core.Domain;

using CQRS.Core.Events;

public interface IEventStoreRepository
{
    Task<List<BaseEvent>> FindEventsByAggregateIdAsync(Guid aggregateId);
    Task SaveAsync(EventStoreModel @eventStoreModel);
    Task SaveAsync(IEnumerable<EventStoreModel> eventStoreModels);
}
