namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class EditCommentCommand : BaseCommand
{
    public EditCommentCommand(string raisedBy, Guid commentId, string comment)
    {
        this.CommentId = commentId;
        this.Comment = comment;
    }

    public Guid CommentId { get; set; }
    public string Comment { get; set; }
}
