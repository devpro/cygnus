using Cygnus.Domain.Models;

namespace Cygnus.ConsoleApp.Tasks
{
    internal class ApplicationTaskFactory
    {
        private readonly ServiceProvider _serviceProvider;

        public ApplicationTaskFactory(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITask Create(EntityModel entity)
        {
            if (entity.Source.ProviderType == ProviderTypeModel.SQLServer
                && entity.Destination.ProviderType == ProviderTypeModel.MongoDB)
            {
                return new DataSyncTask(
                    _serviceProvider.GetService<ILogger<DataSyncTask>>() ?? throw new InvalidOperationException("ILogger must be defined in service collection"),
                    _serviceProvider.GetService<Infrastructure.SqlServerClient.SqlServerClientRepository>() ?? throw new InvalidOperationException("SqlServerClientRepository must be defined in service collection"),
                    _serviceProvider.GetService<Infrastructure.MongoDbDriverClient.MongoDbDriverClientRepository>() ?? throw new InvalidOperationException("MongoDbDriverClientRepository must be defined in service collection"));
            }

            throw new InvalidOperationException($"Operation \"{entity.Source.ProviderType}\" -> \"{entity.Destination.ProviderType}\" not implemented");
        }
    }
}
