namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public class CommentEditedEventHandler : IEventHandler<CommentEditedEvent>
{
    public Task OnAsync(CommentEditedEvent @event)
    {
        throw new NotImplementedException();
    }
}
