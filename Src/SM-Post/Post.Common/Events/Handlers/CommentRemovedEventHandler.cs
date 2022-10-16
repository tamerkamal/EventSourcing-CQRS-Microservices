namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;

public class CommentRemovedEventHandler : IEventHandler<CommentRemovedEvent>
{
    public Task OnAsync(CommentRemovedEvent @event)
    {
        throw new NotImplementedException();
    }
}
