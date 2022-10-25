namespace CQRS.Core.Domain;

using CQRS.Core.Events;

public interface IEventStoreRepository
{
    Task<List<BaseEvent>> FindEventsByAggregateIdAsync(Guid aggregateId);
    Task SaveAsync(EventStoreModel @eventStoreModel);
    Task SaveAsync(List<EventStoreModel> eventStoreModels);
    Task<List<EventStoreModel>> FindAllAsync();
}
