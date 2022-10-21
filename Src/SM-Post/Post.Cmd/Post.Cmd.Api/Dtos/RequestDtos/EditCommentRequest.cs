namespace Post.Cmd.Api.Dtos.RequestDtos;

public record EditCommentRequest
{
    public EditCommentRequest(string raisedBy, Guid commentId, string comment)
    {
        RaisedBy = raisedBy;
        CommentId = commentId;
        Comment = comment;
    }

    public string RaisedBy { get; init; }
    public Guid CommentId { get; init; }
    public string Comment { get; init; }
}
