namespace Otus.Teaching.Pcf.Administration.WebHost.MongoConfiguration
{
    public class MongoDbConfiguration
    {
        public static string SectionName => nameof(MongoDbConfiguration);
        
        public string DatabaseName { get; set; }
        
        public string EmployeesCollectionName { get; set; }
        
        public string RolesCollectionName { get; set; }
    }
}