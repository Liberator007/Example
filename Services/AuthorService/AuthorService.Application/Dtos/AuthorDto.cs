namespace AuthorService.Application.Dtos;

public record AuthorDto(
    Guid Id,
    string Username,
    string Email,
    string Password);
