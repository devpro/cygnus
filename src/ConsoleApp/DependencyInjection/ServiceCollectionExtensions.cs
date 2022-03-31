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

        internal static IServiceCollection AddDomain(this IServiceCollection services, ApplicationConfiguration configuration)
        {
            services.AddSingleton(configuration.DatabaseConfiguration);
            return services;
        }

        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<Serialization.YamlService>();
            return services;
        }

        internal static IServiceCollection AddInfrastructure(this IServiceCollection services, ApplicationConfiguration configuration)
        {
            // SQL Server
            services.AddTransient<Infrastructure.SqlServerClient.SqlServerClientRepository>();

            // MongoDB
            services.AddSingleton<Infrastructure.MongoDbDriverClient.MongoDbDriverClientConfiguration>();
            services.TryAddScoped<Infrastructure.MongoDbDriverClient.MongoClientFactory>();
            services.AddTransient<Infrastructure.MongoDbDriverClient.MongoDbDriverClientRepository>();

            return services;
        }
    }
}
