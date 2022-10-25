using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands;

public class RestoreAppDbCommand : BaseCommand
{
    public RestoreAppDbCommand(string? raisedBy) : base(raisedBy)
    {
    }
}
