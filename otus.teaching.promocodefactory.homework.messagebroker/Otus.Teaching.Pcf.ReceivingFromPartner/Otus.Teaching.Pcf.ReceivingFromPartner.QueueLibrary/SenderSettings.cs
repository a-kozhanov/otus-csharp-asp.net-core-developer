using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.QueueLibrary
{
    public class SenderSettings
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
    }
}
