namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;

public class CommentEditedEventHandler : IEventHandler<CommentEditedEvent>
{
    public Task OnAsync(CommentEditedEvent @event)
    {
        throw new NotImplementedException();
    }
}
