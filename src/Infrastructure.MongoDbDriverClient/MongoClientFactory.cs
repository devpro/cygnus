using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    public class MongoClientFactory
    {
        private readonly MongoDbDriverClientConfiguration _configuration;

        public MongoClientFactory(MongoDbDriverClientConfiguration configuration)
        {
            _configuration = configuration;
            RegisterConventions();
        }

        public MongoClient CreateClient(string connectionStringName)
        {
            //TODO
            return new MongoClient(_configuration.ConnectionString);
        }

        private void RegisterConventions()
        {
            if (_configuration.SerializationConventions == null
                || !_configuration.SerializationConventions.Any())
            {
                return;
            }

            var pack = new ConventionPack();

            if (_configuration.SerializationConventions.Contains("CamelCaseElementName"))
            {
                pack.Add(new CamelCaseElementNameConvention());
            }

            if (_configuration.SerializationConventions.Contains("EnumAsString"))
            {
                pack.Add(new EnumRepresentationConvention(BsonType.String));
            }

            if (_configuration.SerializationConventions.Contains("IgnoreExtraElements"))
            {
                pack.Add(new IgnoreExtraElementsConvention(true));
            }

            if (_configuration.SerializationConventions.Contains("IgnoreNullValues"))
            {
                pack.Add(new IgnoreIfNullConvention(true));
            }

            ConventionRegistry.Register("Cygnus conventions", pack, t => true);
        }
    }
}
