// main

await Parser.Default.ParseArguments<CommandLineOptions>(args)
    .MapResult(
        (CommandLineOptions opts) => RunOptionsAndReturnExitCode(opts),
        errs => Task.FromResult(HandleParseError())
    );

// private methods

async Task<int> RunOptionsAndReturnExitCode(CommandLineOptions opts)
{
    try
    {
        var appConfiguration = new ApplicationConfiguration(LoadConfiguration(opts));

        using var serviceProvider = CreateServiceProvider(opts, appConfiguration);

        var yamlService = serviceProvider.GetRequiredService<Cygnus.Serialization.YamlService>();
        var rootModel = yamlService.ReadFile(opts.DefinitionFile);

        var factory = new ApplicationTaskFactory(serviceProvider);

        foreach (var entity in rootModel.Entities)
        {
            var task = factory.Create(entity);
            await task.ProcessAsync(entity);
        }

        return 0;
    }
    catch (Exception exc)
    {
        Console.WriteLine($"An error occured: {exc.Message}");
        if (opts.IsVerbose)
        {
            Console.WriteLine(exc.StackTrace);
        }
        return -2;
    }
}

static int HandleParseError()
{
    return -2;
}

IConfigurationRoot LoadConfiguration(CommandLineOptions opts)
{
    var builder = new ConfigurationBuilder();

    if (!string.IsNullOrEmpty(opts.ConfigurationFile) && File.Exists(opts.ConfigurationFile))
    {
        builder.AddJsonFile(opts.ConfigurationFile, true, true);
    }

    return builder.AddEnvironmentVariables()
        .Build();
}

static ServiceProvider CreateServiceProvider(CommandLineOptions opts, ApplicationConfiguration appConfiguration)
{
    var serviceCollection = new ServiceCollection()
        .AddLogging(opts)
        .AddDomain(appConfiguration)
        .AddServices()
        .AddInfrastructure(appConfiguration);

    return serviceCollection.BuildServiceProvider();
}
