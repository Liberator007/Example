using System.Text;
using System.Text.Json;

namespace EventBus.RabbitMQ.Contracts
{
    public record PostCreated(Guid Id, string Name);
}
