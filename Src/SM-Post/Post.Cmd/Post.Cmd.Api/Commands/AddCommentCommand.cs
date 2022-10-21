namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class AddCommentCommand: BaseCommand
{
    public AddCommentCommand(string raisedBy, Guid commentId, string comment)
    {
        CommentId = commentId;
        Comment = comment;
        RaisedBy = raisedBy;
    }

    public Guid CommentId { get; set; }
    public string Comment { get; set; }
}
