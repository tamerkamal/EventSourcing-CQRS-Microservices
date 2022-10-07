namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;

public class RemoveCommentCommandHandler
{
    public void RemoveComment(Guid commentId, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can't remove a comment on a removed post!");
        }

        if (!_comments[commentId].Item2.Equals(commenter, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can't remove a comment added by someone else!");
        }

        RaiseEvent(new CommentRemovedEvent(commenter, commentId) { Id = _id });
    }
}
