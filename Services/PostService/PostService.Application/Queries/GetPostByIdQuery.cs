using MediatR;
using PostService.Application.Dtos;
using PostService.Domain.ValueObjects;

namespace AuthorService.Application.Queries;

public record GetPostByIdQuery(PostId PostId) : IRequest<PostDto>;