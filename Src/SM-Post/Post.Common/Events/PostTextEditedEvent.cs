namespace Post.Common.Events;

using CQRS.Core.Events;

public class PostTextEditedEvent : BaseEvent
{
    public PostTextEditedEvent(string raisedBy, string text) : base(nameof(PostTextEditedEvent), raisedBy)
    {
        this.Text = text;
    }

    public string Text { get; set; }
}
