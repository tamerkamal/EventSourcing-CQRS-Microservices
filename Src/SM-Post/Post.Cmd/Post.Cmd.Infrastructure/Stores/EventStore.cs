namespace Post.Cmd.Infrastructure.Stores;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Execptions;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Aggregates;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    public async Task<List<BaseEvent>> GetEventsOrderedDescendingByVersionAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindEventsByAggregateIdAsync(aggregateId);

        if (eventStream?.Any() is not true)
        {
            throw new AggregateNotFoundException("Incorrect Post Id provided");
        }

        return eventStream.OrderByDescending(x => x.Version).ToList();
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await GetEventsOrderedDescendingByVersionAsync(aggregateId);

        // Optimistic concurrency check 
        if (expectedVersion != -1 && eventStream[0].Version != expectedVersion)
        {
            throw new ConcurrencyException();
        }

        var newEventVersion = expectedVersion + 1;

        List<EventStoreModel> eventStoreModels = new();

        foreach (var @event in events)
        {
            var eventType = @event.GetType().Name;

            @event.Version = newEventVersion;

            EventStoreModel eventStroreModel = new()
            {
                AggregateId = aggregateId,
                AggregateType = nameof(PostAggregate),
                AggregateVersion = @event.Version,
                EventData = @event,
                EventType = eventType,
                TimeStamp = DateTime.UtcNow
            };

            eventStoreModels.Add(eventStroreModel);

            newEventVersion++;
        }

        await _eventStoreRepository.SaveAsync(eventStoreModels);
    }
}
