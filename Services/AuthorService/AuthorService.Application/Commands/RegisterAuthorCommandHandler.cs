using MediatR;

namespace AuthorService.Application.Commands;

//public class RegisterAuthorCommandHandler : IRequestHandler<RegisterAuthorCommand, Guid>
//{
//    private readonly IAuthorRepository _authorRepository;
//    private readonly IPasswordHasher _passwordHasher;

//    public RegisterAuthorCommandHandler(IAuthorRepository authorRepository, IPasswordHasher passwordHasher)
//    {
//        _authorRepository = authorRepository;
//        _passwordHasher = passwordHasher;
//    }

//    public async Task<Guid> Handle(RegisterAuthorCommand command, CancellationToken cancellationToken)
//    {
//        var author = Author.Create(
//            Guid.NewGuid(),
//            command.Username,
//            command.Email,
//            _passwordHasher.HashPassword(command.Password)
//        );

//        await _authorRepository.AddAsync(author);
//        return author.Id;
//    }
//}

public class RegisterAuthorCommandHandler : IRequestHandler<RegisterAuthorCommand, AuthorId>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterAuthorCommandHandler(IAuthorRepository authorRepository, IPasswordHasher passwordHasher)
    {
        _authorRepository = authorRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthorId> Handle(RegisterAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = Author.Create(
            AuthorId.Of(Guid.NewGuid()),
            command.Username,
            command.Email,
            _passwordHasher.HashPassword(command.Password)
        );

        await _authorRepository.AddAsync(author);
        return author.Id;
    }
}