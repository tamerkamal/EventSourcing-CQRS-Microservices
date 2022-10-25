namespace Post.Cmd.Infrastructure.Handlers;

using System;
using System.Threading.Tasks;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;

public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
{

    private readonly IEventStore _eventStore;
    private readonly IEventProducer _eventProducer;

    public EventSourcingHandler(IEventStore eventStore, IEventProducer eventProducer)
    {
        _eventStore = eventStore;
        _eventProducer = eventProducer;
    }

    public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
    {
        PostAggregate postAggregate = new();

        var eventsOrderedByVersion = await _eventStore.GetEventsOrderedByVersionAsync(aggregateId);

        if (eventsOrderedByVersion?.Any() is not true)
        {
            return postAggregate;
        }

        postAggregate.ReplayEvents(eventsOrderedByVersion);

        postAggregate.Version = eventsOrderedByVersion.Select(x => x.Version).Last();

        return postAggregate;
    }

    public async Task RepublishEventsAsync()
    {
        var aggregateIds = await _eventStore.GetAggregateIdsAsync();

        if (aggregateIds?.Any() is not true) { return; }

        var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

        foreach (var aggregateId in aggregateIds)
        {
            var aggregate = await GetByIdAsync(aggregateId);

            if (aggregate is null || !aggregate.IsActive) { continue; }

            var events = await _eventStore.GetEventsOrderedByVersionAsync(aggregateId);

            foreach (var @event in events)
            {

                await _eventProducer.ProduceAsync(topic, @event);
            }
        }
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedEvents(), aggregate.Version);
        aggregate.ConsiderEventChangesAsCommitted();
    }
}
