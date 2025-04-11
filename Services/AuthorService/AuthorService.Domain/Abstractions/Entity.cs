using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthorService.Domain.Abstractions;
public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    //public DateTime? CreatedAt { get; set; }
}
