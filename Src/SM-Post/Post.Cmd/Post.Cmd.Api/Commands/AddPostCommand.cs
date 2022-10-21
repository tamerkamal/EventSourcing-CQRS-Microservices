namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class AddPostCommand : BaseCommand
{
    public AddPostCommand(string raisedBy, string text)
    {
        // assumed that author is same as the user who added the post (Raised the event)
        this.RaisedBy = string.IsNullOrEmpty(raisedBy) ? "System" : raisedBy;
        this.Text = text;
    }

    public string Text { get; set; }
}
