namespace Post.Cmd.Infrastructure.Handlers;

using System;
using System.Threading.Tasks;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Aggregates;

public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
{

    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
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

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedEvents(), aggregate.Version);
        aggregate.ConsiderEventChangesAsCommitted();
    }
}
