using System.Text;
using System.Text.Json;

namespace EventBusRabbitMQ.Models
{
    public record AuthorCreated(Guid Id, string Username);
}
