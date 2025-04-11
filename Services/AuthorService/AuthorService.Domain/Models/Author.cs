using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorService.Domain.Models;

[Table("Author")]
public class Author : Entity<AuthorId>
{
    public string Username { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;

    public static Author Create(
        AuthorId authorId, string username, string email, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var author = new Author
        {
            Id = authorId,
            Username = username,
            Email = email,
            Password = password,
        };

        return author;
    }
}