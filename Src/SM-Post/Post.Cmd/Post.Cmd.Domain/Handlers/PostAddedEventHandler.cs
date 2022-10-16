namespace Post.Cmd.Domain.Handlers;

using CQRS.Core.Handlers;
using Post.Cmd.Domain.Repositories;
using Post.Common.Entities;
using Post.Common.Events;

public class PostAddedEventHandler : IEventHandler<PostAddedEvent>
{

    private readonly IPostCmdRepository _postRepository;

    public PostAddedEventHandler(IPostCmdRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task OnAsync(PostAddedEvent @event)
    {
        PostEntity postEntity = new(
        postId: @event.Id,
        text: @event.Text,
        author: @event.Author,
        addedOn: @event.RaisedOn
        );

        await _postRepository.CreateAsync(postEntity);
    }
}
