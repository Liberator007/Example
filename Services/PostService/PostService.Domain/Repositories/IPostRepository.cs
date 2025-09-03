namespace PostService.Domain.Services;

public interface IPostRepository
{
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(PostId id);
    Task<Post?> GetByIdAsync(PostId id);
    Task<List<Post>> GetAllPostsByAuthorAsync(Guid authorId);
    Task SaveChangesAsync();
}
