namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class EditCommentCommand: BaseCommand
{   
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string Username { get; set; }
}
