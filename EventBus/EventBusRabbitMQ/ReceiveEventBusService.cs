using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
//using RabbitMQ.Client.Events;
//using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ
{
    public interface IReceiveEventBusService
    {
        Task ReceiveMessage<T>(T obj);
        Task ReceiveMessage();
    }

    public class ReceiveEventBusService : IReceiveEventBusService
    {
        public async Task ReceiveMessage<T>(T obj)
        {
            var message = JsonSerializer.Serialize<T>(obj);
            //await SendMessage(message);
        }

        public async Task ReceiveMessage()
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

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    
                    consumer.ReceivedAsync += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        // Console.WriteLine($" [x] Received {message}");
                        return Task.CompletedTask;
                    };
                }
            }
        }
    }
}
