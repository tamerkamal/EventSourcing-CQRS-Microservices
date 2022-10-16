
namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public class PostLikedEventHandler : IEventHandler<PostLikedEvent>
{
    public Task OnAsync(PostLikedEvent @event)
    {
        throw new NotImplementedException();
    }
}
