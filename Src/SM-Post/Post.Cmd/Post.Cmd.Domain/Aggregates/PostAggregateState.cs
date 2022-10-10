using CQRS.Core.Domain;

namespace Post.Cmd.Domain.Aggregates;

public partial class PostAggregate : AggregateRoot
{
    #region State Propeties

    public bool IsActive { get; set; }
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

    #endregion

    #region  Constructors

    public PostAggregate()
    {

    }

    public PostAggregate(Guid id, string author, bool isActive = true)
    {
        this.Id = id;
        this._author = author;
        this.IsActive = isActive;
    }

    #endregion
}
