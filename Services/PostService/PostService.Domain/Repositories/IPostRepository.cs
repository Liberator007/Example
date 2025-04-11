namespace PostService.Domain.Services;

public interface IPostRepository
{
    Task AddAsync(Post post);
    Task<Post?> GetByIdAsync(PostId id);
    Task<List<Post>> GetAllAsync(Guid AuthorId);
    //Task UpdateAsync(Post post);
    Task SaveChangesAsync();
}
