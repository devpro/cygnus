using System.Collections.Generic;
using System.Threading.Tasks;
using Cygnus.Domain.Models;

namespace Cygnus.Domain.Repositories
{
    public interface IDataSynchronizationRepository
    {
        Task<List<Dictionary<string, string>>> ReadAsync(SourceModel source, List<FieldModel> fields, string correlationField);

        Task WriteAsync(List<Dictionary<string, string>> data, DestinationModel destination);
    }
}
