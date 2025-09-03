using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Commands;
using System.Security.Claims;

namespace PostService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            var authorId = Request.Headers["X-Author-Id"].ToString();
            //var authorId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            if (string.IsNullOrEmpty(authorId))
                return Unauthorized();

            command = command with { AuthorId = Guid.Parse(authorId) };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
