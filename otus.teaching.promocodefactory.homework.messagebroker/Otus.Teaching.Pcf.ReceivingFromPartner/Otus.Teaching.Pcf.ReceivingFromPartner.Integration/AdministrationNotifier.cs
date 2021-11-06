using System;

using Otus.Teaching.Pcf.ReceivingFromPartner.QueueLibrary;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration
{
    public class AdministrationNotifier
    {
        private readonly QueueSender _queueSender;

        public AdministrationNotifier(QueueSender queueSender)
        {
            _queueSender = queueSender;
        }
        
        public void NotifyAdminAboutPartnerManagerPromoCode(Guid partnerManagerId)
        {
            var id = partnerManagerId.ToString();

            _queueSender.Send(id, "AdministratorPromoCode");
        }
    }
}