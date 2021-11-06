using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using Otus.Teaching.Pcf.Administration.QueueLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.HostedServices
{
    public static class AdminQueueListenerModule
    {
        public static void AddAdminQueueListener(this IServiceCollection services, IConfiguration configuration)
        {
            var listenerSection = configuration.GetSection("Queue").GetSection("Listener");
            var brokerSection = configuration.GetSection("Queue").GetSection("Broker");
            var queueSettings = new ReceiverSettings
            {
                Queue = listenerSection["Queue"],
                Durable = bool.Parse(listenerSection["Durable"]),
                AutoAck = bool.Parse(listenerSection["AutoAck"]),
                Exchange = listenerSection["Exchange"],
                ExchangeType = listenerSection["ExchangeType"], 
                Keys = listenerSection["RoutingKeys"]?.Split(';')?.ToList()
            };
            var brokerSettings = new BrokerSettings
            {
                Host = brokerSection["Host"],
                Port = int.Parse(brokerSection["Port"]),
                User = brokerSection["User"],
                Password = brokerSection["Password"]
            };

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddSingleton(brokerSettings);
            services.AddSingleton(queueSettings);
            services.AddHostedService<AdminQueueListener>();
        }
    }
}
