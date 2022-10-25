namespace Post.Cmd.Api.RestoreAppDbComand;

using CQRS.Core.Commands;

public class RemovePostCommand: BaseCommand
{
    public RemovePostCommand(string raisedBy) : base(raisedBy)
    {
    }
}
