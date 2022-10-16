namespace CQRS.Core.Handlers;

using CQRS.Core.Events;

public interface IEventHandler<Event> where Event : BaseEvent
{
    public Task OnAsync(Event @event);
}
