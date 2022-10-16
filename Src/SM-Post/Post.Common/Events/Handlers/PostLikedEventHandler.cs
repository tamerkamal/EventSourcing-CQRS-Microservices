
namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;

public class PostLikedEventHandler : IEventHandler<PostLikedEvent>
{
    public Task OnAsync(PostLikedEvent @event)
    {
        throw new NotImplementedException();
    }
}
