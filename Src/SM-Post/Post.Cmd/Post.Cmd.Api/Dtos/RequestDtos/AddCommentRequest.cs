namespace Post.Cmd.Api.Dtos.RequestDtos;

public record AddCommentRequest
{
    public AddCommentRequest(string raisedBy, string comment)
    {
        RaisedBy = raisedBy;
        Comment = comment;
    }

    public Guid PostId { get; set; }
    public string RaisedBy { get; init; }
    public string Comment { get; init; }
}
