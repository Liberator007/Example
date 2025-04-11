namespace AuthorService.Infrastructure.Interfaces;

public interface IJwtService
{
    string GenerateToken(Author author);
}