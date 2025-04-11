using AuthorService.Domain.ValueObjects;
using MediatR;
using PostService.Application.Dtos;
using PostService.Domain.ValueObjects;

namespace AuthorService.Application.Queries;

public record GetPostsByAuthorQuery(Guid AuthorId) : IRequest<List<PostDto>>;