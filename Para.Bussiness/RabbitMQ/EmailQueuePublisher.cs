using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.RabbitMQ
{
    public class EmailQueuePublisher :IEmailQueuePublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public EmailQueuePublisher()
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://zekmbidm:le4AK0Y-DbdpI2eGoyVCQBkPl3k14KYR@cow.rmq2.cloudamqp.com/zekmbidm") };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "email_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                 routingKey: "email_queue",
                                 basicProperties: null,
                                 body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
