namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;

public class PostRemovedEventHandler : IEventHandler<PostRemovedEvent>
{
    public Task OnAsync(PostRemovedEvent @event)
    {
        throw new NotImplementedException();
    }
}
