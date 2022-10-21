namespace Post.Cmd.Api.Dtos.RequestDtos;

public record EditPostTextRequest
{
    public EditPostTextRequest(string raisedBy, string text)
    {
        Text = text;
        RaisedBy = raisedBy;
    }

    public string RaisedBy { get; init; }
    public string Text { get; init; }
}
