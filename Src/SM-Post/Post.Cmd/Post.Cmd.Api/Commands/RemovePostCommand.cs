namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class RemovePostCommand: BaseCommand
{
    public RemovePostCommand(string raisedBy) : base(raisedBy)
    {
    }
}
