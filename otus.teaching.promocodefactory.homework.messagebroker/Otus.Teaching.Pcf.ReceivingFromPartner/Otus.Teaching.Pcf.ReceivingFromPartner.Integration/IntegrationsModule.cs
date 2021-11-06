
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Otus.Teaching.Pcf.ReceivingFromPartner.QueueLibrary;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration
{
    public static class IntegrationsModule
    {
        public static void AddIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            var senderSection = configuration.GetSection("Integration").GetSection("Sender");
            var brokerSection = configuration.GetSection("Integration").GetSection("Broker");
            bool.TryParse(senderSection["Durable"], out var durable);
            var queueSettings = new SenderSettings
            {
                Queue = senderSection["Queue"],
                Durable = durable,
                ExchangeType = senderSection["ExchangeType"],
                Exchange = senderSection["Exchange"]
            };
            var brokerSettings = new BrokerSettings
            {
                Host = brokerSection["Host"], 
                Port = int.Parse(brokerSection["Port"]), 
                User = brokerSection["User"], 
                Password = brokerSection["Password"]
            };

            services.AddScoped((provider)=>
            {
                return new QueueSender(queueSettings, brokerSettings);
            });
            services.AddScoped<AdministrationNotifier>(); 
            services.AddScoped<GivingPromoCodeToCustomerNotifier>();
        }
    }
}