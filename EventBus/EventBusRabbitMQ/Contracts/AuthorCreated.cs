using System.Text;
using System.Text.Json;

namespace EventBus.RabbitMQ.Contracts
{
    public record AuthorCreated(Guid Id, string Username, string Email);
}
