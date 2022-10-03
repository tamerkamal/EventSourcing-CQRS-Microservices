namespace CQRS.Core.Domain;

using CQRS.Core.Events;
public abstract class AggregateRoot
{
    //public Guid Id { get; protected set; }

    protected Guid _id;

    public Guid Id
    {
        get { return _id; }
    }

    private readonly List<BaseEvent> _uncommitedEventChanges = new();

    public int Version { get; set; } = -1;
    public IEnumerable<BaseEvent> GetUncommittedChnegs()
    {
        return _uncommitedEventChanges;
    }

    public void MarkChangesAsCommitted(string parameter)
    {
        _uncommitedEventChanges.Clear();
    }

    private void ApplyChange(BaseEvent @event, bool isNewEvent)
    {
        var applyMethod = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

        if (applyMethod is null)
        {
            throw new ArgumentNullException(nameof(applyMethod), $"The 'Apply' method was not found in the aggregate for {@event.GetType().Name}");
        }

        applyMethod.Invoke(this, new object[] { @event });

        if (isNewEvent)
        {
            _uncommitedEventChanges.Add(@event);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    public void ReplayEventStoreEvents(IEnumerable<BaseEvent> eventStoreEvents)
    {
        foreach (var @event in eventStoreEvents)
        {
            ApplyChange(@event, false);
        }
    }
}