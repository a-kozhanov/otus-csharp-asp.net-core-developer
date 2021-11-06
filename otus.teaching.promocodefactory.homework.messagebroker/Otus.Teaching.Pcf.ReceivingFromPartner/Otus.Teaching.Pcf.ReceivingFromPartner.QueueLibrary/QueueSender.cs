using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.QueueLibrary
{
    public class QueueSender
    {
        private readonly SenderSettings _queueSettings;
        private readonly BrokerSettings _brokerSettings;

        public QueueSender(SenderSettings queueSettings, BrokerSettings brokerSettings)
        {
            _queueSettings = queueSettings;
            _brokerSettings = brokerSettings;
        }

        public void Send(string message, string routingKey)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _brokerSettings.Host,
                Port = _brokerSettings.Port,
                UserName = _brokerSettings.User,
                Password = _brokerSettings.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                if (!string.IsNullOrEmpty(_queueSettings.Queue)){
                    channel.QueueDeclare(queue: _queueSettings.Queue,
                                         durable: _queueSettings.Durable,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null); 
                }
                else
                {
                    channel.ExchangeDeclare(exchange: _queueSettings.Exchange,
                                    type: _queueSettings.ExchangeType);
                }

                channel.BasicPublish(
                    exchange: _queueSettings.Exchange,
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
