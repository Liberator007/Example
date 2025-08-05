using MediatR;
using PostService.Application.Dtos;

namespace AuthorService.Application.Queries;

public record GetPostsByAuthorQuery(Guid AuthorId) : IRequest<List<PostDto>>;