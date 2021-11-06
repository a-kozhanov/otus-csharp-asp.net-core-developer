using Microsoft.Extensions.Hosting;
using Otus.Teaching.Pcf.GivingToCustomer.QueueLibrary;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.QueueLibrary
{
    public abstract class QueueListener : BackgroundService
    {
        private readonly TimeSpan _listenTimeout = TimeSpan.FromSeconds(10);
        private readonly BrokerSettings _brokerSettings;
        private readonly ReceiverSettings _queueSettings;

        public QueueListener(BrokerSettings brokerSettings, ReceiverSettings queueSettings)
        {
            _brokerSettings = brokerSettings;
            _queueSettings = queueSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _brokerSettings.Host,
                    Port = _brokerSettings.Port,
                    UserName = _brokerSettings.User,
                    Password = _brokerSettings.Password
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: _queueSettings.Queue,
                    durable: _queueSettings.Durable,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                if (!string.IsNullOrEmpty(_queueSettings.Exchange))
                {
                    AddWithExchange(channel);
                }

                Consume(channel);

                await Task.Delay(_listenTimeout, stoppingToken);
            }
        }

        private void AddWithExchange(IModel channel)
        {
            if (_queueSettings.Keys != null && _queueSettings.Keys.Any())
            {
                foreach (var key in _queueSettings.Keys)
                {

                    channel.QueueBind(
                        queue: _queueSettings.Queue,
                        exchange: _queueSettings.Exchange,
                        routingKey: key,
                        arguments: null);
                }
            }
            else
            {
                channel.QueueBind(
                    queue: _queueSettings.Queue,
                    exchange: _queueSettings.Exchange,
                    routingKey: "",
                    arguments: null);
            }
        }

        private void Consume(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await ProcessMessageAsync(message);
            };
            channel.BasicConsume(queue: _queueSettings.Queue,
                                    autoAck: _queueSettings.AutoAck,
                                    consumer: consumer);
        }

        protected abstract Task ProcessMessageAsync(string message);
    }
}
