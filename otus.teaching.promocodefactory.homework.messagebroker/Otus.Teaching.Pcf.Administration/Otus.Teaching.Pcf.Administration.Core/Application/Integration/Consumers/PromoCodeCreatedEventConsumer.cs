using System.Threading.Tasks;
using MassTransit;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Services;
using Otus.Teaching.Pcf.Integration.Contracts;

namespace Otus.Teaching.Pcf.Administration.Core.Application.Integration.Consumers
{
    public class PromoCodeCreatedEventConsumer:IConsumer<PromoCodeCreatedEvent>
    {
        private readonly IEmployeeService _employeeService;

        public PromoCodeCreatedEventConsumer(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task Consume(ConsumeContext<PromoCodeCreatedEvent> context)
        {
            if (context.Message.PartnerManagerId != null)
                await _employeeService.UpdateAppliedPromocodesAsync(context.Message.PartnerManagerId.Value);
        }
    }
}