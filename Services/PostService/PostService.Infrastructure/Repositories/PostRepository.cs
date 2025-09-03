using AuthorService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Services;
using PostService.Domain.ValueObjects;
using PostService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Post post)
        {
            //post.Id = Guid.NewGuid();
            post.CreatedAt = DateTime.UtcNow;
            await _context.Posts.AddAsync(post);
        }

        public async Task UpdateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public async Task DeleteAsync(PostId postId)
        {
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
                throw new UnauthorizedAccessException("You cannot delete this post.");

            _context.Posts.Remove(post);
        }

        public async Task<Post?> GetByIdAsync(PostId postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<List<Post>> GetAllPostsByAuthorAsync(Guid authorId)
        {
            return await _context.Posts.Where(p => p.AuthorId == authorId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
