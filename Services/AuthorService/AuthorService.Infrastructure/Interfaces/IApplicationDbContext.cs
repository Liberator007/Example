using Microsoft.EntityFrameworkCore;

namespace AuthorService.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Author> Authors { get; }

    //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}