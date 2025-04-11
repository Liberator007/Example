using MediatR;

namespace AuthorService.Application.Commands;

//public class RegisterAuthorCommand : IRequest<Guid>
//{
//    public string Username { get; set; }
//    public string Email { get; set; }
//    public string Password { get; set; }
//}

public class RegisterAuthorCommand : IRequest<AuthorId>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}