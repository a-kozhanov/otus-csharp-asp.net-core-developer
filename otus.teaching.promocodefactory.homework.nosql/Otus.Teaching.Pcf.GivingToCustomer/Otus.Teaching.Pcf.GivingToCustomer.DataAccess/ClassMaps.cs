using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess
{
    public static class ClassMaps
    {
        public static void Configure()
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
            BsonClassMap.RegisterClassMap<BaseEntity>(map =>
            {
                map.AutoMap();
                map.MapMember(entity => entity.Id).SetSerializer(new GuidSerializer(BsonType.String));
            });
            BsonClassMap.RegisterClassMap<PromoCode>(map =>
            {
                map.AutoMap();
                map.MapMember(code => code.BeginDate).SetSerializer(new DateTimeSerializer(true));
                map.MapMember(code => code.EndDate).SetSerializer(new DateTimeSerializer(true));
            });
        }
    }
}