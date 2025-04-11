using Microsoft.EntityFrameworkCore;

namespace PostService.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
