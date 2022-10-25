namespace Post.Cmd.Api.RestoreAppDbComand;

using CQRS.Core.Commands;

public class LikePostCommand: BaseCommand
{
    public LikePostCommand(string raisedBy) : base(raisedBy)
    {
    }
}
