namespace AuthorService.Domain.Services;

public interface IAuthorRepository
{
    Task AddAsync(Author author);
    Task<Author> GetByUsernameAsync(string username);
}
