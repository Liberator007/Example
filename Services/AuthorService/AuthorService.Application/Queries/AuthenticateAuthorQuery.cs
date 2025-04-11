using MediatR;

namespace AuthorService.Application.Queries;

public class AuthenticateAuthorQuery : IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
}