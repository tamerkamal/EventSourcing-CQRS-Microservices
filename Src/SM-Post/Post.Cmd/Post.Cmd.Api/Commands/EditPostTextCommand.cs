namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class EditPostTextCommand: BaseCommand
{   
    public string Text { get; set; }
}
