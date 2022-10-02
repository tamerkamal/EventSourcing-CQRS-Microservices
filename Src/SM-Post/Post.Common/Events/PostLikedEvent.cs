namespace Post.Common.Events;

using CQRS.Core.Events;

public class PostLikedEvent : BaseEvent
{
    public PostLikedEvent(string raisedBy) : base(nameof(PostLikedEvent), raisedBy) { }
}
