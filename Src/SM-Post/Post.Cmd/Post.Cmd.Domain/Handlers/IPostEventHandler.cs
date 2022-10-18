namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public interface IPostEventHandler : IEventHandler
{
    Task OnAsync(PostAddedEvent @event);
    Task OnAsync(PostTextEditedEvent @event);
    Task OnAsync(PostLikedEvent @event);
    Task OnAsync(PostRemovedEvent @event);
}
