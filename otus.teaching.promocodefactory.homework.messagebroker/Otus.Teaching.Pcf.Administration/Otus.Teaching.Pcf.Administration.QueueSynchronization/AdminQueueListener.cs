using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.QueueLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.QueueSynchronization
{
    public class AdminQueueListener : QueueListener
    {
        private readonly IRepository<Employee> _employeeRepository;

        public AdminQueueListener(IRepository<Employee> employeeRepository, BrokerSettings brokerSettings, 
            ReceiverSettings receiverSettings) : base(brokerSettings, receiverSettings)
        {
            _employeeRepository = employeeRepository;
        }

        protected override async Task ProcessMessageAsync(string message)
        {
            Guid.TryParse(message, out var guid);

            var employee = await _employeeRepository.GetByIdAsync(guid);

            if (employee != null)
            {
                employee.AppliedPromocodesCount++;

                await _employeeRepository.UpdateAsync(employee);
            }
        }
    }
}
