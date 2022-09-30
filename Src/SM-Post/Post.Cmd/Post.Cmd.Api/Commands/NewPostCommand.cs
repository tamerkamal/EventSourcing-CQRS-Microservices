namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class NewPostCommand: BaseCommand
{
    public string Author { get; set; }
    public string Text { get; set; }
}
