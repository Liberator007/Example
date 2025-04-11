using AuthorService.Domain.ValueObjects;
using PostService.Domain.ValueObjects;

namespace PostService.Application.Dtos;

public class PostDto
{
    public PostId Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public Guid AuthorId { get; set; }

    // Преобразование сущности Post в PostDto
    public static PostDto FromPost(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Name = post.Name,
            Text = post.Text,
            AuthorId = post.AuthorId
        };
    }
}