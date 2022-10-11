namespace CQRS.Core.Domain;

public abstract class BaseEntity
{
    public BaseEntity(DateTimeOffset addedOn, string addedBy = "System")
    {
        this.AddedOn = addedOn;
        this.AddedBy = addedBy;
    }

    public DateTimeOffset AddedOn { get; set; }
    public string AddedBy { get; set; }
}
