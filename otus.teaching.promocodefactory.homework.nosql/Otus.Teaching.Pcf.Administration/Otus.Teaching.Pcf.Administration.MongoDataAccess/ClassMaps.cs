using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Otus.Teaching.Pcf.Administration.Core.Domain;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;

namespace Otus.Teaching.Pcf.Administration.MongoDataAccess
{
    public static class BaseEntityClassMap
    {
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(map =>
            {
                map.AutoMap();
                map.MapMember(entity => entity.Id).SetSerializer(new GuidSerializer(BsonType.String));
            });
        }
    }

    public static class EmployeeClassMap
    {
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<Employee>(map =>
            {
                map.AutoMap();
                map.MapMember(emp => emp.RoleId).SetSerializer(new GuidSerializer(BsonType.String));
                map.UnmapMember(employee => employee.Role);
            });
        }
    }

    public static class RoleClassMap
    {
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<Role>(map =>
            {
                map.AutoMap();
            });
        }
    }
}