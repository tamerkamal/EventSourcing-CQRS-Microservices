using CQRS.Core.Execptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.RestoreAppDbComand;
using Post.Cmd.Api.Dtos.RequestDtos;
using Post.Common.Dtos;

namespace Comment.Cmd.Api.Controllers.CommentControllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EditCommentController : ControllerBase
{
    private readonly ILogger<EditCommentController> _logger;
    private readonly ICommandDispatcher _commeandDispatcher;

    public EditCommentController(ILogger<EditCommentController> logger, ICommandDispatcher commeandDispatcher)
    {
        _logger = logger;
        _commeandDispatcher = commeandDispatcher;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditCommentAsync(Guid id, EditCommentRequest request)
    {
        try
        {
            EditCommentCommand editCommentCommand = new(request.RaisedBy, request.CommentId, request.Comment) { Id = id };

            await _commeandDispatcher.SendAsync(editCommentCommand);

            return Ok(new BaseResponse { Message = "Comment Edited Successfully!" });
        }

        catch (AggregateNotFoundException aggregateNotFoundException)
        {
            _logger.Log(LogLevel.Warning, aggregateNotFoundException, "Could not retrieve aggregate, client passed an invalid Comment Id!");

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
            const string SAFE_ERROR_MESSAGE = "An error occured while Editing the comment!";
            _logger.Log(LogLevel.Error, exception, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}
