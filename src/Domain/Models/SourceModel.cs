namespace Cygnus.Domain.Models
{
    public class SourceModel
    {
        public ProviderTypeModel Provider { get; set; }

        public string Query { get; set; } = string.Empty;
    }
}
