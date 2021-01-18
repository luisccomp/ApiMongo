using System;
using ApiMongo.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ApiMongo.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration.GetConnectionString("MongoDB")));
                var client = new MongoClient(settings);

                DB = client.GetDatabase(configuration["Database"]);

                MapClasses();
            }
            catch (Exception e)
            {
                throw new MongoException("It was not possible to connect to MongoDB", e);
            }
        }

        public void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            RegisterClass<Infectado>();
        }

        public void RegisterClass<T> ()
        {
            BsonClassMap.RegisterClassMap<T>(i =>
            {
                i.AutoMap();
                i.SetIgnoreExtraElements(true);
            });
        }
    }
}