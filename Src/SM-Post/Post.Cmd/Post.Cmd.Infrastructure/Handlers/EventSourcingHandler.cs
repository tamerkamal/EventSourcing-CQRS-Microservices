using System.Runtime.InteropServices.ComTypes;
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

        var eventsOrderedDescendingByVersion = await _eventStore.GetEventsOrderedDescendingByVersionAsync(aggregateId);

        if (eventsOrderedDescendingByVersion?.Any() is not true)
        {
            return postAggregate;
        }

        postAggregate.ReplayEvents(eventsOrderedDescendingByVersion);

        postAggregate.Version = eventsOrderedDescendingByVersion.Select(x => x.Version).First();

        return postAggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedEvents(), aggregate.Version);
        aggregate.ConsiderEventChangesAsCommitted();
    }
}
