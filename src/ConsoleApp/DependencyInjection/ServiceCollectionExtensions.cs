namespace Cygnus.ConsoleApp.DependencyInjection
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddLogging(this IServiceCollection services, CommandLineOptions opts)
        {
            services.AddLogging(builder =>
             {
                 builder
                     .AddFilter("Microsoft", opts.IsVerbose ? LogLevel.Information : LogLevel.Warning)
                     .AddFilter("System", opts.IsVerbose ? LogLevel.Information : LogLevel.Warning)
                     .AddFilter("Cygnus", opts.IsVerbose ? LogLevel.Debug : LogLevel.Information)
                     .AddConsole();
             });

            return services;
        }

        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<YamlService>();

            return services;
        }

        internal static IServiceCollection AddInfrastructure(this IServiceCollection services, ApplicationConfiguration configuration)
        {
            // Domain
            services.AddSingleton(configuration.DatabaseConfiguration);

            // SQL Server
            services.AddTransient<Infrastructure.SqlServerClient.SqlServerClientRepository>();

            // MongoDB
            // TODO
            var mongoDbDriverClientConfiguration = new Infrastructure.MongoDbDriverClient.MongoDbDriverClientConfiguration()
            {
                ConnectionString = configuration.DatabaseConfiguration.ConnectionStrings["MongoDbLocal"]
            };
            services.AddSingleton(mongoDbDriverClientConfiguration);
            services.TryAddScoped<Infrastructure.MongoDbDriverClient.MongoClientFactory>();
            services.AddTransient<Infrastructure.MongoDbDriverClient.MongoDbDriverClientRepository>();

            return services;
        }
    }
}
