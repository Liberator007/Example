using MediatR;
using PostService.Application.Dtos;
using PostService.Domain.ValueObjects;

namespace PostService.Application.Commands;
public record CreatePostCommand(string Name, string Text, Guid AuthorId) : IRequest<PostDto>;