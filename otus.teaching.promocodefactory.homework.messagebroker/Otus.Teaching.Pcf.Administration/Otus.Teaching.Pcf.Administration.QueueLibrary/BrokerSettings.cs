using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.Administration.QueueLibrary
{
    public class BrokerSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
