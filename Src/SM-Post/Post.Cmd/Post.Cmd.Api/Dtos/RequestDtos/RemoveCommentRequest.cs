namespace Post.Cmd.Api.Dtos.RequestDtos;

public record RemoveCommentRequest
{
    public RemoveCommentRequest(Guid commentId, string raisedBy)
    {
        CommentId = commentId;
        RaisedBy = raisedBy;
    }

    public Guid CommentId { get; init; }

    public string RaisedBy { get; set; }
}
