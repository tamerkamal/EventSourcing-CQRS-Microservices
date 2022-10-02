namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class AddCommentCommand: BaseCommand
{
    public AddCommentCommand(string raisedBy, string comment) : base(raisedBy)
    {
        this.Comment = comment;
    }

    public string Comment { get; set; }
}
