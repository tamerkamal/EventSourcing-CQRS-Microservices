namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class AddPostCommand : BaseCommand
{
    public AddPostCommand(string raisedBy, string text) : base(raisedBy)
    {
        Text = text;
    }

    public string Text { get; set; }
}
