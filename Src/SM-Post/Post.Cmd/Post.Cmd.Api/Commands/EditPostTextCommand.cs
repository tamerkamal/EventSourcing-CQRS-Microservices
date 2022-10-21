namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class EditPostTextCommand: BaseCommand
{
    public EditPostTextCommand(string raisedBy, string text)
    {
        this.RaisedBy = raisedBy;
        this.Text = text;
    }

    public string Text { get; set; }
}
