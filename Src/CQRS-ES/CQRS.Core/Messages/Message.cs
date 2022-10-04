namespace CQRS.Core.Messages;

public abstract class Message
{
    public Message()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id {get;set;}
}
