namespace Post.Cmd.Domain.Handlers;

using Post.Common.Events;

public interface ICommentEventHandler
{
    Task OnAsync(CommentAddedEvent @event);
    Task OnAsync(CommentEditedEvent @event);
    Task OnAsync(CommentRemovedEvent @event);
}
