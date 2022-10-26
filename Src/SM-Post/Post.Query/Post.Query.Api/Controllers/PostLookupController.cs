using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Common.Dtos;
using Post.Common.Entities;
using Post.Query.Api.Dtos;
using Post.Query.Api.Queries;

namespace Post.Query.Api.Controllers;

[ApiController]
[Route("api/v1/post//[controller]")]
public class PostLookupController : ControllerBase
{
    private readonly ILogger<PostLookupController> _logger;
    private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

    public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
    {
        _logger = logger;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllPostsAsync()
    {
        try
        {
            var posts = (await _queryDispatcher.SendAsync(new GetAllPostsQuery()))?.ToList();

            if (posts?.Any() is not true)
            {
                return NoContent();
            }

            var totalPosts = posts.Count();

            return Ok(new PostLookupResponse
            {
                Posts = posts,
                Message = $"Successfully returned {totalPosts} post(s)"
            });
        }
        catch (System.Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error occured while retrieving posts!";
            _logger.LogError(ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
    [HttpGet("{postId}")]
    public async Task<ActionResult> GetPostByIdAsync(Guid postId)
    {
        try
        {
            var post = (await _queryDispatcher.SendAsync(new GetPostByIdQuery(postId)))?.SingleOrDefault();

            if (post is null)
            {
                return NotFound();
            }

            return Ok(new PostLookupResponse
            {
                Posts = new List<PostEntity> { post },
                Message = $"Successfully returned post"
            });
        }
        catch (System.Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error occured while retrieving post";
            _logger.LogError(ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }

    [HttpGet("{author}")]
    public async Task<ActionResult> GetPostsByAuthorAsync(string author)
    {
        try
        {
            var posts = (await _queryDispatcher.SendAsync(new GetPostsByAuthorQuery(author)))?.ToList();

            if (posts?.Any() is not true)
            {
                return NoContent();
            }

            return Ok(new PostLookupResponse
            {
                Posts = posts,
                Message = $"Successfully returned {posts.Count} post(s)"
            });
        }
        catch (System.Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error occured while retrieving post";
            _logger.LogError(ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }

    [HttpGet("havingComments")]
    public async Task<ActionResult> GetPostsHavingCommentsAsync()
    {
        try
        {
            var posts = (await _queryDispatcher.SendAsync(new GetPostsHavingCommentsQuery()))?.ToList();

            if (posts?.Any() is not true)
            {
                return NoContent();
            }

            return Ok(new PostLookupResponse
            {
                Posts = posts,
                Message = $"Successfully returned {posts.Count} post(s)"
            });
        }
        catch (System.Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error occured while retrieving posts";
            _logger.LogError(ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
    [HttpGet("minimumNumOfLikes/{num}")]
    public async Task<ActionResult> GetPostsHavingLikesAsync(int num = 0)
    {
        try
        {
            var posts = (await _queryDispatcher.SendAsync(new GetPostsHavingLikesQuery(num)))?.ToList();

            if (posts?.Any() is not true)
            {
                return NoContent();
            }

            return Ok(new PostLookupResponse
            {
                Posts = posts,
                Message = $"Successfully returned {posts.Count} post(s)"
            });
        }
        catch (System.Exception ex)
        {
            const string SAFE_ERROR_MESSAGE = "Error occured while retrieving posts";
            _logger.LogError(ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE
            });
        }
    }
}
