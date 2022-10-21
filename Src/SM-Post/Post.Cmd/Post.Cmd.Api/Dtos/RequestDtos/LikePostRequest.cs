namespace Post.Cmd.Api.Dtos.RequestDtos;

public record LikePostRequest
{
    public LikePostRequest(string raisedBy)
    {
        RaisedBy = raisedBy;
    }

    public string RaisedBy { get; init; }
}
