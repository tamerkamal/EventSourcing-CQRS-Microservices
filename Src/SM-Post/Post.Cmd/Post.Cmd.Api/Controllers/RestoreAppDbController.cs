using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Common.Dtos;

namespace Post.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/appDb/[controller]")]
public class RestoreAppDbController : ControllerBase
{
    private readonly ILogger<RestoreAppDbController> _logger;
    private readonly ICommandDispatcher _commeandDispatcher;

    public RestoreAppDbController(ILogger<RestoreAppDbController> logger, ICommandDispatcher commeandDispatcher)
    {
        _logger = logger;
        _commeandDispatcher = commeandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> RestoreAppDbAsync(string raisedBy)
    {
        try
        {
            RestoreAppDbCommand restoreAppDbCommand = new(raisedBy);

            await _commeandDispatcher.SendAsync(restoreAppDbCommand);

            return StatusCode(StatusCodes.Status201Created, new BaseResponse
            {
                Message = "App. Database restored successfully!"
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
            const string SAFE_ERROR_MESSAGE = "An error occured while restoring the app. database!";
            _logger.Log(LogLevel.Error, exception, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}
