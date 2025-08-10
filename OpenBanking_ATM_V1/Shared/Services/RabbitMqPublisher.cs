using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

using OpenBanking_ATM_V1.Shared.Events;
using Microsoft.Extensions.Configuration;
 

namespace OpenBanking_ATM_V1.Shared.Services
{
    public class RabbitMqPublisher
{
    private readonly IConfiguration _config;
    private readonly IModel _channel;

    public RabbitMqPublisher(IConfiguration config)
    {
        _config = config;
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ:HostName"],
            UserName = _config["RabbitMQ:UserName"],
            Password = _config["RabbitMQ:Password"]
        };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.ExchangeDeclare(_config["RabbitMQ:Exchange"], ExchangeType.Direct, durable: true);
    }

    public void PublishAtmCreated(AtmCreatedEvent atmEvent)
    {
        Publish(atmEvent, "atm.created");
    }

    public void PublishAtmAttributeCreated(AtmAttributeCreatedEvent atmattributeEvent)
    {
        Publish(atmattributeEvent, "atm.attribute.created");
    }

    private void Publish<T>(T evt, string routingKey)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt));
        _channel.BasicPublish(
            exchange: _config["RabbitMQ:Exchange"],
            routingKey: routingKey,
            basicProperties: null,
            body: body
        );
    }
}

}
