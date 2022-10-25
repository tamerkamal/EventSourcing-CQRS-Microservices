namespace Post.Cmd.Api.RestoreAppDbComand;

using CQRS.Core.Commands;

public class EditCommentCommand : BaseCommand
{
    public EditCommentCommand(string raisedBy, Guid commentId, string comment) : base(raisedBy)
    {
        this.CommentId = commentId;
        this.Comment = comment;
    }

    public Guid CommentId { get; set; }
    public string Comment { get; set; }
}
