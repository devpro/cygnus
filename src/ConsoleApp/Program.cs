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
        var appConfiguration = new ApplicationConfiguration(LoadConfiguration());

        using var serviceProvider = CreateServiceProvider(opts, appConfiguration);

        var yamlService = serviceProvider.GetRequiredService<YamlService>();
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

IConfigurationRoot LoadConfiguration()
{
    return new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();
}

static ServiceProvider CreateServiceProvider(CommandLineOptions opts, ApplicationConfiguration appConfiguration)
{
    var serviceCollection = new ServiceCollection()
        .AddLogging(opts)
        .AddServices()
        .AddInfrastructure(appConfiguration);

    return serviceCollection.BuildServiceProvider();
}
