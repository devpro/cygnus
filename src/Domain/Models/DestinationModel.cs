using System.Collections.Generic;

namespace Cygnus.Domain.Models
{
    public class DestinationModel
    {
        public string Name { get; set; } = string.Empty;

        public ProviderTypeModel ProviderType { get; set; } = ProviderTypeModel.None;

        public string Database { get; set; } = string.Empty;

        public string Collection { get; set; } = string.Empty;

        public string CorrelationField { get; set; } = string.Empty;

        public List<FieldModel> Fields { get; set; } = new List<FieldModel>();
    }
}
