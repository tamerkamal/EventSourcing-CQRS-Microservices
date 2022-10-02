namespace CQRS.Core.Commands;

using CQRS.Core.Messages;

public abstract class BaseCommand: Message
{
    protected BaseCommand(string? raisedBy)
    {
        this.RaisedBy = string.IsNullOrEmpty(raisedBy) ? "System" : raisedBy;
    }

    public string RaisedBy { get; set; }
}