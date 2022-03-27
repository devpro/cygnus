using System.Collections.Generic;

namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    public class MongoDbDriverClientConfiguration
    {
        //TODO
        public string ConnectionString { get; set; } = string.Empty;

        public List<string> SerializationConventions =>
            new List<string>
            {
                "CamelCaseElementName",
                "EnumAsString",
                "IgnoreExtraElements",
                "IgnoreNullValues"
            };
    }
}
