namespace Post.Cmd.Infrastructure.Handlers.EventHandlers;

using Post.Cmd.Domain.Handlers;
using Post.Cmd.Domain.Repositories;
using Post.Common.Entities;
using Post.Common.Events;

public class PostEventHandler : IPostEventHandler
{
    private readonly IPostCmdRepository _postCmdRepository;

    public PostEventHandler(IPostCmdRepository postCmdRepository)
    {
        _postCmdRepository = postCmdRepository;
    }

    public async Task OnAsync(PostAddedEvent @event)
    {
        PostEntity postEntity = new(
        postId: @event.Id,
        text: @event.Text,
        author: @event.Author,
        addedOn: @event.RaisedOn
        );

        await _postCmdRepository.CreateAsync(postEntity);
    }

    public async Task OnAsync(PostTextEditedEvent @event)
    {
        var postEntity = await _postCmdRepository.GetByIdAsync(@event.Id);

        postEntity.Text = @event.Text;

        await _postCmdRepository.UpdateAsync(postEntity);
    }

    public async Task OnAsync(PostLikedEvent @event)
    {
        var postEntity = await _postCmdRepository.GetByIdAsync(@event.Id);

        postEntity.Likes++;

        await _postCmdRepository.UpdateAsync(postEntity);
    }

    public async Task OnAsync(PostRemovedEvent @event)
    {
        await _postCmdRepository.DeleteAsync(@event.Id);
    }
}
