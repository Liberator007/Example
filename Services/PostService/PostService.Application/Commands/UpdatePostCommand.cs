namespace PostService.Application.Commands;
public record UpdatePostCommand(PostId PostId, string Name, string Text) : IRequest<Unit>;