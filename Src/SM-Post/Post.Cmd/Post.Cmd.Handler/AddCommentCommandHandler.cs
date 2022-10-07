namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;

public class AddCommentCommandHandler
{
    public void AddComment(string comment, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }

        RaiseEvent(new CommentAddedEvent(commenter, Guid.NewGuid(), comment) { Id = _id });
    }
}
