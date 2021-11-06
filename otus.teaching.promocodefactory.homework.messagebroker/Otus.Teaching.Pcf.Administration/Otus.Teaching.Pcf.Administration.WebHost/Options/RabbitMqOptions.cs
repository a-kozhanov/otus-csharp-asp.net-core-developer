namespace Otus.Teaching.Pcf.Administration.WebHost.Options
{
    public class RabbitMqOptions
    {
        public const string SectionName = nameof(RabbitMqOptions);
        
        public string Host { get; set; }
        
        public ushort Port { get; set; }
        
        public string VirtualHost { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string PromoCodeCreatedEventQueue { get; set; }
        
    }
}