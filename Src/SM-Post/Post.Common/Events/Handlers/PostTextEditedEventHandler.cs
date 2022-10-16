namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;

public class PostTextEditedEventHandler : IEventHandler<PostTextEditedEvent>
{
    public Task OnAsync(PostTextEditedEvent @event)
    {
        throw new NotImplementedException();
    }
}
