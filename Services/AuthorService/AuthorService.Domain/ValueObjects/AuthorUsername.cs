using AuthorService.Domain.Exceptions;
namespace AuthorService.Domain.ValueObjects;

public record AuthorUsername
{
    private const int DefaultLength = 40;
    public string Value { get; }
    private AuthorUsername(string value) => Value = value;
    public static AuthorUsername Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new AuthorUsername(value);
    }
}
