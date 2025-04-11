using MassTransit;
using MediatR;
using PostService.Application.Dtos;
using PostService.Domain.Services;
using PostService.Domain.ValueObjects;

namespace PostService.Application.Commands;
public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreatePostCommandHandler(IPostRepository postRepository, IPublishEndpoint publishEndpoint)
    {
        _postRepository = postRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<PostDto> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var post = Post.Create(
            PostId.Of(Guid.NewGuid()),
            command.Name,
            command.Text,
            command.AuthorId
        );

        await _postRepository.AddAsync(post);

        await _publishEndpoint.Publish(new PostCreated(post.Id.Value, post.Name));
        return PostDto.FromPost(post);
    }
}