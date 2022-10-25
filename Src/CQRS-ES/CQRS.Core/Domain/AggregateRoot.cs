namespace CQRS.Core.Domain;

using CQRS.Core.Events;

public abstract class AggregateRoot
{
    #region Properties   

    public int Version { get; set; } = -1;
    public Guid Id { get; protected set; }
    private readonly List<BaseEvent> _uncommitedEventChanges = new();

    #endregion

    #region Public methods

    public List<BaseEvent> GetUncommittedEvents()
    {
        return _uncommitedEventChanges;
    }

    public void ConsiderEventChangesAsCommitted()
    {
        _uncommitedEventChanges.Clear();
    }

    public void ReplayEvents(List<BaseEvent> eventStoreEvents)
    {
        foreach (var @event in eventStoreEvents)
        {
            ApplyChange(@event, false);
        }
    }

    #endregion

    #region Protected methods

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    #endregion

    #region Private methods

    private void ApplyChange(BaseEvent @event, bool isNewEvent)
    {
        var currentClassType = this.GetType();
        var eventType = @event.GetType();

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

    #endregion  
}