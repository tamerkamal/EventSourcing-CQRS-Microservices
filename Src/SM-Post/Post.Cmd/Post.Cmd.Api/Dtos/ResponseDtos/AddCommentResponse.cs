using Post.Common.Dtos;

namespace Post.Cmd.Api.Dtos.ResponseDtos;

public class AddCommentResponse : BaseResponse
{
    public Guid CommentId { get; set; }
}
