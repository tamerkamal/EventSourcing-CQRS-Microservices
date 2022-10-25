namespace Post.Query.Api.Dtos;

using Post.Common.Dtos;
using Post.Common.Entities;

public class PostLookupResponse : BaseResponse
{
    public List<PostEntity> Posts { get; set; }


}
