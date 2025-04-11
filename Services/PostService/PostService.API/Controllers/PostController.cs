using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Commands;
using System.Security.Claims;

namespace PostService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            var authorId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            command = command with { AuthorId = AuthorId.Of(Guid.Parse(authorId)) };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
