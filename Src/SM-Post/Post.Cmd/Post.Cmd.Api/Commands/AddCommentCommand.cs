namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class AddCommentCommand: BaseCommand
{
    public AddCommentCommand(string raisedBy, Guid commentId, string comment) : base(raisedBy)
    {
        CommentId = commentId;
        Comment = comment;
    }

    public Guid CommentId { get; set; }
    public string Comment { get; set; }
}
