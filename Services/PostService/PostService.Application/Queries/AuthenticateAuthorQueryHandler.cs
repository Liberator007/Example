using AuthorService.Domain.Models;
using AuthorService.Domain.Services;
using AuthorService.Infrastructure.Interfaces;
using MediatR;

namespace AuthorService.Application.Queries;

public class AuthenticateAuthorQueryHandler : IRequestHandler<AuthenticateAuthorQuery, string>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthenticateAuthorQueryHandler(IAuthorRepository authorRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _authorRepository = authorRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(AuthenticateAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByUsernameAsync(request.Username);

        if (author == null || !_passwordHasher.VerifyPassword(author.Password, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var token = _jwtService.GenerateToken(author);
        return token;
    }
}
