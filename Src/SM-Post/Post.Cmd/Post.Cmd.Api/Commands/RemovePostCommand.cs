namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class RemovePostCommand: BaseCommand
{   
    public string Username { get; set; }
}
