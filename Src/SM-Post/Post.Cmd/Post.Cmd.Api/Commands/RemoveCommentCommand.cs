namespace Post.Cmd.Api.RestoreAppDbComand;

using CQRS.Core.Commands;

public class RemoveCommentCommand: BaseCommand
{
    public RemoveCommentCommand(string raisedBy, Guid commentId) : base(raisedBy)
    {
        CommentId = commentId;
    }

    public Guid CommentId { get; set; }
}
