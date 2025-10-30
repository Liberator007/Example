using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBus.RabbitMQ.MassTransit;
public static class Extentions
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                // Поддерживаем либо plain hostname (например "messagebroker"), либо rabbitmq:// URI
                var hostSetting = configuration["MessageBroker:Host"] ?? "rabbitmq://messagebroker";
                Uri hostUri;

                if (!Uri.TryCreate(hostSetting, UriKind.Absolute, out hostUri) || string.IsNullOrEmpty(hostUri.Scheme))
                {
                    hostUri = new Uri($"rabbitmq://{hostSetting}");
                }

                // Если передали amqp:// — заменим на rabbitmq://
                if (hostUri.Scheme == "amqp")
                {
                    hostUri = new Uri($"rabbitmq://{hostUri.Host}{(hostUri.IsDefaultPort ? "" : $":{hostUri.Port}")}{hostUri.PathAndQuery}");
                }

                configurator.Host(hostUri, host =>
                {
                    host.Username(configuration["MessageBroker:UserName"] ?? "guest");
                    host.Password(configuration["MessageBroker:Password"] ?? "guest");
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
