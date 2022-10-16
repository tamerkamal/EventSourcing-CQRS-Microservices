namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public class PostTextEditedEventHandler : IEventHandler<PostTextEditedEvent>
{
    public Task OnAsync(PostTextEditedEvent @event)
    {
        throw new NotImplementedException();
    }
}
