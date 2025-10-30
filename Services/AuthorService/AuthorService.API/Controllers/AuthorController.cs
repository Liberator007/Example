using AuthorService.Application.Commands;
using AuthorService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthorService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAuthorCommand command)
        {
            var authorId = await _mediator.Send(command);
            return Ok(new { AuthorId = authorId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateAuthorQuery query)
        {
            var token = await _mediator.Send(query);
            return Ok(new { Token = token });
        }
    }
}
