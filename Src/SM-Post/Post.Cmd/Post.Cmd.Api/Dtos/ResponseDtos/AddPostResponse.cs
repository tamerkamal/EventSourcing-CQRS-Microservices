namespace Post.Cmd.Api.Dtos.ResponseDtos;

using Post.Common.Dtos;

public class AddPostResponse : BaseResponse
{
    public Guid PostId { get; set; }
}
