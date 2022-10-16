namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public class PostRemovedEventHandler : IEventHandler<PostRemovedEvent>
{
    public Task OnAsync(PostRemovedEvent @event)
    {
        throw new NotImplementedException();
    }
}
