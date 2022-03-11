namespace Cygnus.ConsoleApp
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
            // SQL Server
            services.AddSingleton(configuration.SqlServerClientConfiguration);
            services.AddTransient<Infrastructure.SqlServerClient.SqlServerClientRepository>();

            // MongoDB
            services.AddSingleton(configuration.MongoDbDriverClientConfiguration);
            services.AddSingleton<Withywoods.Dal.MongoDb.IMongoDbConfiguration>(configuration.MongoDbDriverClientConfiguration);
            services.TryAddTransient<Withywoods.Dal.MongoDb.IMongoClientFactory, Withywoods.Dal.MongoDb.MongoClientFactory>();
            services.TryAddScoped<Withywoods.Dal.MongoDb.IMongoDbContext, Withywoods.Dal.MongoDb.DefaultMongoDbContext>();
            services.AddTransient<Infrastructure.MongoDbDriverClient.MongoDbDriverClientRepository>();

            return services;
        }
    }
}
