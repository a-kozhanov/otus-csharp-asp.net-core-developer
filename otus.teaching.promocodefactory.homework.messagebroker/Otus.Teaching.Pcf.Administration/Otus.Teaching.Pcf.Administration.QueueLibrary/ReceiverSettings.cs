using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.Administration.QueueLibrary
{
    public class ReceiverSettings
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
        public List<string> Keys { get; set; }
        public bool AutoAck { get; set; }
    }
}
