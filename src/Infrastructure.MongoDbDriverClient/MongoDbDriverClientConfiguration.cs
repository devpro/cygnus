using System.Collections.Generic;
using Withywoods.Dal.MongoDb;
using Withywoods.Dal.MongoDb.Serialization;

namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    public class MongoDbDriverClientConfiguration : IMongoDbConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public List<string> SerializationConventions =>
            new List<string>
            {
                ConventionValues.CamelCaseElementName,
                ConventionValues.EnumAsString,
                ConventionValues.IgnoreExtraElements,
                ConventionValues.IgnoreNullValues
            };
    }
}
