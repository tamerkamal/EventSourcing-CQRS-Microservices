namespace Post.Query.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRS.Core.Domain;

[Table("Comment")]
public class CommentEntity : BaseEntity
{
    public CommentEntity(Guid commentId, string comment, bool wasEdited, Guid postId, DateTimeOffset addedOn, string addedBy) : base(addedOn, addedBy)
    {
        this.CommentId = commentId;
        this.Comment = comment;
        this.PostId = postId;
        this.WasEdited = wasEdited;
    }

    [Key]
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public bool WasEdited { get; set; }
    public Guid PostId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore] //To avoid circular reference exception
    public virtual PostEntity Post { get; set; }
}
