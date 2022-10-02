namespace CQRS.Core.Events;

using CQRS.Core.Messages;

public abstract class BaseEvent : Message
{
    protected BaseEvent(string type, string? raisedBy)
    {
        this.Type = type;
        this.RaisedOn = DateTimeOffset.UtcNow;
        this.RaisedBy = string.IsNullOrEmpty(raisedBy) ? "System" : raisedBy;
    }

    public int Version { get; set; }
    public string Type { get; set; }
    public DateTimeOffset RaisedOn { get; }
    public string RaisedBy { get; set; }
}
