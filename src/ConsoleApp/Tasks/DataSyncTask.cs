using Cygnus.Domain.Models;
using Cygnus.Domain.Repositories;

namespace Cygnus.ConsoleApp.Tasks
{
    public class DataSyncTask : ITask
    {
        private readonly ILogger _logger;

        private readonly IDataSynchronizationRepository _sourceRepository;

        private readonly IDataSynchronizationRepository _destinationRepository;

        public DataSyncTask(
            ILogger<DataSyncTask> logger,
            IDataSynchronizationRepository sourceRepository,
            IDataSynchronizationRepository destinationRepository)
        {
            _logger = logger;
            _sourceRepository = sourceRepository;
            _destinationRepository = destinationRepository;
        }

        public async Task ProcessAsync(EntityModel entity)
        {
            _logger.LogInformation("Processing {EntityName} entity starting...", entity.Name);

            var data = await _sourceRepository.ReadAsync(entity.Source, entity.Destination.Fields, entity.Destination.CorrelationField);

            await _destinationRepository.WriteAsync(data, entity.Destination);

            _logger.LogInformation("Processing of {EntityName} entity complete.", entity.Name);
        }
    }
}
