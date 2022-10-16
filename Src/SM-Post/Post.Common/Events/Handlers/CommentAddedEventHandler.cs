namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;

public class CommentAddedEventHandler : IEventHandler<CommentAddedEvent>
{
    public Task OnAsync(CommentAddedEvent @event)
    {
        throw new NotImplementedException();
    }
}
