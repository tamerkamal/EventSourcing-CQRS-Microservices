namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Events;

public interface ICommentEventHandler : IEventHandler
{
    Task OnAsync(CommentAddedEvent @event);
    Task OnAsync(CommentEditedEvent @event);
    Task OnAsync(CommentRemovedEvent @event);
}
