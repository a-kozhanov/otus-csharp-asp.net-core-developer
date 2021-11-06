using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.QueueLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.HostedServices
{
    public class AdminQueueListener : QueueListener
    {
        private readonly IServiceProvider _serviceProvider;

        public AdminQueueListener(IServiceProvider serviceProvider, BrokerSettings brokerSettings, 
            ReceiverSettings receiverSettings) : base(brokerSettings, receiverSettings)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ProcessMessageAsync(string message)
        {
            Guid.TryParse(message, out var guid);

            using var scope = _serviceProvider.CreateScope();
            var employeeRepository = scope.ServiceProvider.GetService<IRepository<Employee>>();
            var employee = await employeeRepository.GetByIdAsync(guid);

            if (employee != null)
            {
                employee.AppliedPromocodesCount++;

                await employeeRepository.UpdateAsync(employee);
            }
        }
    }
}
