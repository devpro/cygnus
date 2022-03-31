using System.Collections.Generic;

namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    public class MongoDbDriverClientConfiguration
    {
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
