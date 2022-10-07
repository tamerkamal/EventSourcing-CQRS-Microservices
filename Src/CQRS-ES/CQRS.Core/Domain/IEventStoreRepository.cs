namespace CQRS.Core.Domain;

using CQRS.Core.Events;

public interface IEventStoreRepository
{
    Task SaveAsync(EventStoreModel @eventStoreModel);
    Task<List<EventStoreModel>> FindByAggregateIdAsync(Guid aggregateId);
}
