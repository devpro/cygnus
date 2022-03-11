using Cygnus.Domain.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Cygnus.ConsoleApp.Services
{
    public class YamlService
    {
        private readonly ILogger _logger;

        public YamlService(ILogger<YamlService> logger)
        {
            _logger = logger;
        }

        public RootModel ReadFile(string filename)
        {
            if (filename == null || !File.Exists(filename))
            {
                throw new ArgumentNullException(nameof(filename), $"File \"{Directory.GetCurrentDirectory()}/{filename}\" doesn't exist");
            }

            var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

            try
            {
                return deserializer.Deserialize<RootModel>(File.ReadAllText(filename));
            }
            catch (Exception exc)
            {
                _logger.LogWarning("An error occured while reading [Filename={Filename}] [ExceptionMessage={ExceptionMessage}]",
                    filename, exc.Message);
                throw;
            }
        }
    }
}
