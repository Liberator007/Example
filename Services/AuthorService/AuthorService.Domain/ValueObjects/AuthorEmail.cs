using AuthorService.Domain.Exceptions;

namespace AuthorService.Domain.ValueObjects;
public record AuthorEmail
{
    private const int DefaultLength = 50;
    public string Value { get; }
    private AuthorEmail(string value) => Value = value;
    public static AuthorEmail Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new AuthorEmail(value);
    }
}
