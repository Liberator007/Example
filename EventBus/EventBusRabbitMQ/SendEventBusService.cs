using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ
{
    public interface ISendEventBusService
    {
        Task SendMessage<T>(T obj);
        Task SendMessage(string message);
    }

    public class SendEventBusService : ISendEventBusService
    {
        public async Task SendMessage<T>(T obj)
        {
            var message = JsonSerializer.Serialize<T>(obj);
            await SendMessage(message);
        }

        public async Task SendMessage(string message)
        {
            // Не забудьте вынести значения "localhost" и "MyQueue"
            // в файл конфигурации
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = await factory.CreateConnectionAsync())
            {
                using (var channel = await connection.CreateChannelAsync())
                {
                    await channel.QueueDeclareAsync(
                        queue: "MyQueue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    await channel.BasicPublishAsync(
                        exchange: "",
                        routingKey: "MyQueue",
                        //basicProperties: null,
                        body: body);
                }
            }
        }
    }
}
