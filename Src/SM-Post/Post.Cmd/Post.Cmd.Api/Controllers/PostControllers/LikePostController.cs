using CQRS.Core.Execptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.RestoreAppDbComand;
using Post.Cmd.Api.Dtos.RequestDtos;
using Post.Common.Dtos;

namespace Post.Cmd.Api.Controllers.PostControllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LikePostController : ControllerBase
{
    private readonly ILogger<LikePostController> _logger;
    private readonly ICommandDispatcher _commeandDispatcher;

    public LikePostController(ILogger<LikePostController> logger, ICommandDispatcher commeandDispatcher)
    {
        _logger = logger;
        _commeandDispatcher = commeandDispatcher;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditPostTextAsync(Guid id, LikePostRequest request)
    {
        try
        {
            LikePostCommand likePostCommand = new(request.RaisedBy) { Id = id };

            await _commeandDispatcher.SendAsync(likePostCommand);

            return Ok(new BaseResponse { Message = "Post liked Successfully!" });
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
            const string SAFE_ERROR_MESSAGE = "An error occured while doing like for the post!";
            _logger.Log(LogLevel.Error, exception, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }

}
