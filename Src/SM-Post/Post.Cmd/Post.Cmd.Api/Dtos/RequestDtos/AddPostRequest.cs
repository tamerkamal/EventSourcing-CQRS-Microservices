namespace Post.Cmd.Api.Dtos.RequestDtos;

public record AddPostRequest
{
    public AddPostRequest(string raisedBy, string text)
    {
        RaisedBy = raisedBy;
        Text = text;
    }

    public string RaisedBy { get; init; }
    public string Text { get; init; }
}