namespace Post.Cmd.Api.Controllers.PostControllers;

using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Dtos.RequestDtos;
using Post.Cmd.Api.Dtos.ResponseDtos;
using Post.Common.Dtos;

[ApiController]
[Route("api/v1/[controller]")]
public class AddPostController : ControllerBase
{
    private readonly ILogger<AddPostController> _logger;
    private readonly ICommandDispatcher _commeandDispatcher;

    public AddPostController(ILogger<AddPostController> logger, ICommandDispatcher commeandDispatcher)
    {
        _logger = logger;
        _commeandDispatcher = commeandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> AddPostAsync(AddPostRequest request)
    {
        try
        {
            AddPostCommand addPostCommand = new(request.RaisedBy, request.Text) { Id = Guid.NewGuid() };

            await _commeandDispatcher.SendAsync(addPostCommand);

            return StatusCode(StatusCodes.Status201Created, new AddPostResponse
            {
                PostId = addPostCommand.Id,
                Message = "New post added successfully!"
            });
        }

        catch (InvalidOperationException invalidOperationException)
        {
            _logger.Log(LogLevel.Warning, invalidOperationException, "Client made a bad request!");

            return BadRequest(new BaseResponse
            {
                Message = invalidOperationException.Message
            });
        }
        catch (Exception exception)
        {
            const string SAFE_ERROR_MESSAGE = "An error occured while adding the new post!";
            _logger.Log(LogLevel.Error, exception, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new AddPostResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}

