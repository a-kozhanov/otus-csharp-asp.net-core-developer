using Otus.Teaching.Pcf.GivingToCustomer.DataAccess;

namespace Otus.Teaching.Pcf.Administration.WebHost.Options
{
    public class MongoDbOptions : IMongoDbOptions
    {
        public const string OptionsName = "MongoOptions";
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}