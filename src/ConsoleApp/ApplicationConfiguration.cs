namespace Cygnus.ConsoleApp
{
    internal class ApplicationConfiguration
    {
        public const string InfrastructureSqlServerConfigKey = "Infrastructure:SqlServer";

        public const string InfrastructureMongoDbDriverConfigKey = "Infrastructure:MongoDbDriver";

        private readonly IConfigurationRoot _configurationRoot;

        public ApplicationConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public Infrastructure.SqlServerClient.SqlServerClientConfiguration SqlServerClientConfiguration =>
            _configurationRoot.GetSection(InfrastructureSqlServerConfigKey).Get<Infrastructure.SqlServerClient.SqlServerClientConfiguration>();

        public Infrastructure.MongoDbDriverClient.MongoDbDriverClientConfiguration MongoDbDriverClientConfiguration =>
            _configurationRoot.GetSection(InfrastructureMongoDbDriverConfigKey).Get<Infrastructure.MongoDbDriverClient.MongoDbDriverClientConfiguration>();
    }
}
