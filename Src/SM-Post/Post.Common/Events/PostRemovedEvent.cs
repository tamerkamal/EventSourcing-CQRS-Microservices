namespace Post.Common.Events;

using CQRS.Core.Events;
public class PostRemovedEvent : BaseEvent
{
    public PostRemovedEvent(string raisedBy) : base(nameof(PostRemovedEvent), raisedBy)
    {

    }
}



