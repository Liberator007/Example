using Microsoft.EntityFrameworkCore;
using PostService.Domain.ValueObjects;
using System.Reflection;
using System.Reflection.Emit;

namespace PostService.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => PostId.Of(value)
            );
    }
}
