using AuthorService.Domain.ValueObjects;
using MediatR;
using PostService.Application.Dtos;
using PostService.Domain.ValueObjects;

namespace PostService.Application.Commands;
public record UpdatePostCommand(PostId PostId, string Name, string Text) : IRequest<Unit>;