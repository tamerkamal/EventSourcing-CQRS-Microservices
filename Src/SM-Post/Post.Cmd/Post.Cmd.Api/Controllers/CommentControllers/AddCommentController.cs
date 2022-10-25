namespace Post.Cmd.Api.Controllers.CommentControllers;

using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.RestoreAppDbComand;
using Post.Cmd.Api.Dtos.RequestDtos;
using Post.Cmd.Api.Dtos.ResponseDtos;
using Post.Common.Dtos;

[ApiController]
[Route("api/v1/[controller]")]
public class AddCommentController : ControllerBase
{
    private readonly ILogger<AddCommentController> _logger;
    private readonly ICommandDispatcher _commeandDispatcher;

    public AddCommentController(ILogger<AddCommentController> logger, ICommandDispatcher commeandDispatcher)
    {
        _logger = logger;
        _commeandDispatcher = commeandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> AddCommentAsync(AddCommentRequest request)
    {
        try
        {
            AddCommentCommand AddCommentCommand = new(request.RaisedBy, Guid.NewGuid(), request.Comment) { Id = request.PostId };

            await _commeandDispatcher.SendAsync(AddCommentCommand);

            return StatusCode(StatusCodes.Status201Created, new AddCommentResponse
            {
                CommentId = AddCommentCommand.CommentId,
                Message = "New comment added successfully!"
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
            const string SAFE_ERROR_MESSAGE = "An error occured while adding the new comment!";
            _logger.Log(LogLevel.Error, exception, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new AddCommentResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}
