namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class RemoveCommentCommand: BaseCommand
{
    public RemoveCommentCommand(string raisedBy, Guid commentId)
    {
        this.CommentId = commentId;
    }

    public Guid CommentId { get; set; }
}
