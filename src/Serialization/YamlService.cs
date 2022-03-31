using System;
using System.IO;
using Cygnus.Domain.Models;
using Microsoft.Extensions.Logging;
using SharpYaml.Serialization;

namespace Cygnus.Serialization
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

            var serializerSettings = new SerializerSettings
            {
                NamingConvention = new CamelCaseNamingConvention()
            };
            var serializer = new Serializer(serializerSettings);

            try
            {
                return serializer.Deserialize<RootModel>(File.ReadAllText(filename));
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
