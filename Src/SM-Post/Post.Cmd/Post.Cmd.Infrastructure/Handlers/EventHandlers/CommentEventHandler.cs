namespace Post.Cmd.Infrastructure.Handlers.EventHandlers;

using Post.Cmd.Domain.Handlers;
using Post.Cmd.Domain.Repositories;
using Post.Common.Entities;
using Post.Common.Events;

public class CommentEventHandler : ICommentEventHandler
{
    private readonly ICommentCmdRepository _commentCmdRepository;

    public CommentEventHandler(ICommentCmdRepository commentCmdRepository)
    {
        _commentCmdRepository = commentCmdRepository;
    }

    public async Task OnAsync(CommentAddedEvent @event)
    {
        CommentEntity commentEntity = new(
            commentId: @event.CommentId,
            comment: @event.Comment,
            wasEdited: false,
            postId: @event.Id,
            addedOn: @event.RaisedOn,
            addedBy: @event.RaisedBy
        );

        await _commentCmdRepository.CreateAsync(commentEntity);
    }

    public async Task OnAsync(CommentEditedEvent @event)
    {
        var commentEntity = await _commentCmdRepository.GetByIdAsync(@event.CommentId);

        commentEntity.Comment = @event.Comment;
        commentEntity.CommentId = @event.CommentId;
        commentEntity.PostId = @event.Id;
        commentEntity.WasEdited = true;

        await _commentCmdRepository.UpdateAsync(commentEntity);
    }

    public async Task OnAsync(CommentRemovedEvent @event)
    {
        await _commentCmdRepository.DeleteAsync(@event.CommentId);
    }
}
