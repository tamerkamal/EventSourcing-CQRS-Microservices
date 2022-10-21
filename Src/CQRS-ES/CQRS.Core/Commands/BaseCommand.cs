namespace CQRS.Core.Commands;

using CQRS.Core.Messages;

public abstract class BaseCommand: Message
{

#nullable enable

    public string? RaisedBy { get; set; }
}
