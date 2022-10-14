namespace Post.Cmd.Domain.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRS.Core.Domain;

[Table("Post")]
public class PostEntity : BaseEntity
{
    public PostEntity(Guid postId, string text, string author, DateTimeOffset addedOn) : base(addedOn, author)
    {
        this.PostId = postId;
        this.Text = text;
        this.Author = author;
        this.Likes = 0;
    }

    [Key]
    public Guid PostId { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }

    public virtual ICollection<CommentEntity> Comments { get; set; }
}
