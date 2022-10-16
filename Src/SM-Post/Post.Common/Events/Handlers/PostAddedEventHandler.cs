namespace Post.Common.Events.Handlers;

using CQRS.Core.Handlers;
using Post.Common.Entities;

public class PostAddedEventHandler : IEventHandler<PostAddedEvent>
{

    //private readonly IPostRepository _postRepository;

    public async Task OnAsync(PostAddedEvent @event)
    {
        PostEntity post = new(
        postId: @event.Id,
        text: @event.Text,
        author: @event.Author,
        addedOn: @event.RaisedOn
        );

        //await 
    }
}
