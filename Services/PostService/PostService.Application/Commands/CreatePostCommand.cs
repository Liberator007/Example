using MediatR;
using PostService.Application.Dtos;
using PostService.Domain.ValueObjects;
using BuildingBlocks.CQRS;

namespace PostService.Application.Commands;
public record CreatePostCommand(string Name, string Text, Guid AuthorId) : IRequest<PostDto>;

public record CreateOrderCommand(PostDto Order)
    : ICommand<CreatePostResult>;

public record CreatePostResult(Guid Id);