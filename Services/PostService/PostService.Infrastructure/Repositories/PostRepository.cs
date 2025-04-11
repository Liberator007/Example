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

        public async Task AddAsync(Post post) => await _context.Posts.AddAsync(post);
        public async Task<Post?> GetByIdAsync(PostId id) => await _context.Posts.FindAsync(id);
        public async Task<List<Post>> GetAllAsync(Guid AuthorId) => await _context.Posts.Where(p => p.AuthorId == AuthorId).ToListAsync();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
