namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public class CommentAddedEventHandler : IEventHandler<CommentAddedEvent>
{
    public Task OnAsync(CommentAddedEvent @event)
    {
        throw new NotImplementedException();
    }
}
