namespace PostService.Domain.Models;
public class Post : Entity<PostId>
{
    public string Name { get; private set; } = default!;
    public string Text { get; private set; } = default!;
    public DateTime Date { get; private set; } = default!;
    public Guid AuthorId { get; private set; } = default!;

    public static Post Create(PostId postId, string name, string text, Guid authorId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        var post = new Post
        {
            Id = postId,
            Name = name,
            Text = text,
            Date = DateTime.UtcNow,
            AuthorId = authorId
        };

        return post;
    }
    public void Update(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
