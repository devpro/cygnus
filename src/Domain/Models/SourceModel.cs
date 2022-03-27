namespace Cygnus.Domain.Models
{
    public class SourceModel
    {
        public string Name { get; set; } = string.Empty;

        public ProviderTypeModel ProviderType { get; set; } = ProviderTypeModel.None;

        public string Database { get; set; } = string.Empty;

        public string Query { get; set; } = string.Empty;
    }
}
