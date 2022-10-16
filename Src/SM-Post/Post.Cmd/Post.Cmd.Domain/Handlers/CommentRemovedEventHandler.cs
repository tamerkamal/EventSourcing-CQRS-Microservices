namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public class CommentRemovedEventHandler : IEventHandler<CommentRemovedEvent>
{
    public Task OnAsync(CommentRemovedEvent @event)
    {
        throw new NotImplementedException();
    }
}
