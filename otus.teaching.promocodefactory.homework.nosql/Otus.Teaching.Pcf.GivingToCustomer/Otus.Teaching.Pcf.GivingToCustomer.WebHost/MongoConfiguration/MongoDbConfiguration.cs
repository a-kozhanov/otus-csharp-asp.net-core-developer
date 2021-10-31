namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.MongoConfiguration
{
    public class MongoDbConfiguration
    {
        public static string SectionName => nameof(MongoDbConfiguration);
        public string DatabaseName { get; set; }
        public string CustomersCollectionName { get; set; }
        public string PreferencesCollectionName { get; set; }
        public string PromoCodesCollectionName { get; set; }
    }
}