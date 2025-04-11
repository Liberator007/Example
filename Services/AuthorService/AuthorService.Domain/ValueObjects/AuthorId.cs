using AuthorService.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AuthorService.Domain.ValueObjects;
public record AuthorId
{
    public Guid Value { get; }
    private AuthorId(Guid value) => Value = value;
    public static AuthorId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("AuthorId cannot be empty.");
        }

        return new AuthorId(value);
    }
}
