namespace Post.Cmd.Api.RestoreAppDbComand;

using CQRS.Core.Commands;

public class EditPostTextCommand: BaseCommand
{
    public EditPostTextCommand(string raisedBy, string text) : base(raisedBy)
    {
        Text = text;
    }

    public string Text { get; set; }
}
