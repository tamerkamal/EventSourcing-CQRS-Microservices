using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Dtos.RequestDtos;
using Post.Cmd.Api.Commands;
using CQRS.Core.Execptions;
using Post.Common.Dtos;

namespace Post.Cmd.Api.Controllers.CommentControllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RemoveCommentController : ControllerBase
{
    private readonly ILogger<RemoveCommentController> _logger;
    private readonly ICommandDispatcher _commeandDispatcher;

    public RemoveCommentController(ILogger<RemoveCommentController> logger, ICommandDispatcher commeandDispatcher)
    {
        _logger = logger;
        _commeandDispatcher = commeandDispatcher;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveCommentAsync(Guid id, RemoveCommentRequest request)
    {
        try
        {
            RemoveCommentCommand RemoveCommentCommand = new(request.RaisedBy, request.CommentId) { Id = id };

            await _commeandDispatcher.SendAsync(RemoveCommentCommand);

            return Ok(new BaseResponse { Message = "Comment Removed Successfully!" });
        }

        catch (AggregateNotFoundException aggregateNotFoundException)
        {
            _logger.Log(LogLevel.Warning, aggregateNotFoundException, "Could not retrieve aggregate, client passed an invalid Post Id!");

            return BadRequest(new BaseResponse
            {
                Message = aggregateNotFoundException.Message
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
            const string SAFE_ERROR_MESSAGE = "An error occured while Removing the comment!";
            _logger.Log(LogLevel.Error, exception, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}
