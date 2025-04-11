using System.Text;
using System.Text.Json;

namespace EventBusRabbitMQ.Models
{
    public record PostCreated(Guid Id, string Name);
}
