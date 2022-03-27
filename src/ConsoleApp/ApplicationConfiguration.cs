namespace Cygnus.ConsoleApp
{
    internal class ApplicationConfiguration
    {
        public const string DatabasesConfigKey = "Databases";

        private readonly IConfigurationRoot _configurationRoot;

        public ApplicationConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public Domain.Repositories.DatabaseConfiguration DatabaseConfiguration =>
            _configurationRoot.GetSection(DatabasesConfigKey).Get<Domain.Repositories.DatabaseConfiguration>();
    }
}
