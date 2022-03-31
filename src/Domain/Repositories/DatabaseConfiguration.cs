using System.Collections.Generic;

namespace Cygnus.Domain.Repositories
{
    public class DatabaseConfiguration
    {
        public Dictionary<string, string> ConnectionStrings { get; set; } = new Dictionary<string, string>();
    }
}
