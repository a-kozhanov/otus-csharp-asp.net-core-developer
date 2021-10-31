namespace Otus.Teaching.Pcf.Administration.MongoDataAccess
{
    public interface IMongoDbOptions
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}