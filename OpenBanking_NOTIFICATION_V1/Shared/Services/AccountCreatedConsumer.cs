using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using OpenBanking_NOTIFICATION_V1.Shared.Events;
namespace OpenBanking_NOTIFICATION_V1.Shared.Services
{
    public class AccountCreatedConsumer : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly EmailSenderService _emailSender;

        public AccountCreatedConsumer(IConfiguration config , EmailSenderService emailSender)
        {
            _config = config;
            _emailSender = emailSender;
        }

       protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    int maxAttempts = 5;
    int delayBetweenAttempts = 5000; // 5 seconds
    int attempt = 0;

    while (attempt < maxAttempts && !stoppingToken.IsCancellationRequested)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:HostName"],
                UserName = _config["RabbitMQ:UserName"],
                Password = _config["RabbitMQ:Password"]
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare("account_exchange", ExchangeType.Direct, durable: true);
            channel.QueueDeclare("notification_queue", durable: true, exclusive: false, autoDelete: false);
            channel.QueueBind("notification_queue", "account_exchange", "account.created");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var accountEvent = JsonConvert.DeserializeObject<AccountCreatedEvent>(message);

                await _emailSender.SendAccountCreatedEmail(accountEvent.AccountId, accountEvent.Label);
            };

            channel.BasicConsume("notification_queue", autoAck: true, consumer);
            return; // Success
        }
        catch (Exception ex)
        {
            attempt++;
            Console.WriteLine($"❌ Attempt {attempt}: Failed to connect to RabbitMQ. Retrying in {delayBetweenAttempts / 1000}s...");
            await Task.Delay(delayBetweenAttempts, stoppingToken);
        }
    }

    Console.WriteLine("❌ RabbitMQ connection failed after multiple attempts.");
}

    }
}
