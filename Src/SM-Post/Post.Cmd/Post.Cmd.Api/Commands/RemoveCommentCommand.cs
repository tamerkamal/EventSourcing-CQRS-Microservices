namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class RemoveCommentCommand: BaseCommand
{   
    public Guid CommentId { get; set; }   
    public string Username { get; set; }
}
