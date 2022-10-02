namespace Post.Cmd.Api.Commands;

using CQRS.Core.Commands;

public class LikePostCommand: BaseCommand
{
    public LikePostCommand(string raisedBy) : base(raisedBy)
    {

    }
}
