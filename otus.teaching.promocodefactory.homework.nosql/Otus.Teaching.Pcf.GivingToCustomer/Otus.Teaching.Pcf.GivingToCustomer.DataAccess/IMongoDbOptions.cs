namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess
{
    public interface IMongoDbOptions
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}