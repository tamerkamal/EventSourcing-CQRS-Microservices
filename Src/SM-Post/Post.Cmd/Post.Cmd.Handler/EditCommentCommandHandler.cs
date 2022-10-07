namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;

public class EditCommentCommandHandler
{
    public void EditComment(Guid commentId, string comment, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not edit comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }

        if (!_comments[commentId].Item2.Equals(commenter, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can't edit a comment added by someone else!");
        }

        RaiseEvent(new CommentEditedEvent(commenter, commentId, comment) { Id = _id });
    }
}
