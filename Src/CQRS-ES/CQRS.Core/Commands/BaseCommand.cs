namespace CQRS.Core.Commands;

using CQRS.Core.Messages;

public abstract class BaseCommand: Message
{
#nullable enable
    protected BaseCommand(string? raisedBy)
    {
        RaisedBy = raisedBy;
    }

    public string? RaisedBy { get; set; } = "System";
}
