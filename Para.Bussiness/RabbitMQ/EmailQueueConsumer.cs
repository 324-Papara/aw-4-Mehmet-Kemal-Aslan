using Hangfire;
using Para.Bussiness.Notifications;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace Para.Bussiness.RabbitMQ
{
    public class EmailQueueConsumer : IEmailQueueConsumer
    {
        private readonly INotificationService _notificationService;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public EmailQueueConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://zekmbidm:le4AK0Y-DbdpI2eGoyVCQBkPl3k14KYR@cow.rmq2.cloudamqp.com/zekmbidm") }; 
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "email_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailMessage = JsonSerializer.Deserialize<EmailMessage>(message);

                _notificationService.SendEmail(emailMessage.Subject, emailMessage.Email, emailMessage.Content);
            };

            _channel.BasicConsume(queue: "email_queue",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }

        public class EmailMessage
        {
            public string Subject { get; set; }
            public string Email { get; set; }
            public string Content { get; set; }
        }
    }
}
