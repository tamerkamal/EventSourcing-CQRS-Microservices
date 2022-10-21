namespace Post.Cmd.Infrastructure.Stores;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Execptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventProducer = eventProducer;
    }

    public async Task<List<BaseEvent>> GetEventsOrderedByVersionAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindEventsByAggregateIdAsync(aggregateId);

        if (eventStream?.Any() is not true)
        {
            throw new AggregateNotFoundException("Incorrect Post Id provided");
        }

        return eventStream.OrderBy(x => x.Version).ToList();
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = expectedVersion == -1 ? null : await GetEventsOrderedByVersionAsync(aggregateId);

        // Optimistic concurrency check 
        if (expectedVersion != -1 && eventStream.Last().Version != expectedVersion)
        {
            throw new ConcurrencyException();
        }

        var newEventVersion = expectedVersion + 1;

        List<EventStoreModel> eventStoreModels = new();

        var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

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

            await _eventStoreRepository.SaveAsync(eventStroreModel);

            await _eventProducer.ProduceAsync(topic, @event);

            newEventVersion++;
        }
    }
}
