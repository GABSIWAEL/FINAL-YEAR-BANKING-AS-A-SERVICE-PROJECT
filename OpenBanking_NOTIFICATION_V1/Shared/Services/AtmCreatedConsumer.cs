using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using OpenBanking_NOTIFICATION_V1.Shared.Events;

namespace OpenBanking_NOTIFICATION_V1.Shared.Services
{
    public class AtmCreatedConsumer : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly EmailSenderService _emailSender;

        public AtmCreatedConsumer(IConfiguration config, EmailSenderService emailSender)
        {
            _config = config;
            _emailSender = emailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ListenAsync(
                "notification_atm_queue",
                _config["RabbitMQConnections:ATMEvents:RoutingKey"],
                async (msg) =>
                {
                    var evt = JsonConvert.DeserializeObject<AtmCreatedEvent>(msg);
                    await _emailSender.SendAtmCreatedEmail(evt);
                },
                stoppingToken
            );
        }

        private async Task ListenAsync(string queue, string routingKey, Func<string, Task> handle, CancellationToken token)
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQConnections:ATMEvents:HostName"],
                UserName = _config["RabbitMQConnections:ATMEvents:UserName"],
                Password = _config["RabbitMQConnections:ATMEvents:Password"]
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var exchangeName = _config["RabbitMQConnections:ATMEvents:Exchange"];
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true);
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            channel.QueueBind(queue, exchangeName, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                await handle(message);
            };

            channel.BasicConsume(queue, autoAck: true, consumer);
        }
    }
}
