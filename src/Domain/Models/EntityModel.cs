namespace Cygnus.Domain.Models
{
    public class EntityModel
    {
        public string Name { get; set; } = string.Empty;

        public SourceModel Source { get; set; } = new SourceModel();

        public DestinationModel Destination { get; set; } = new DestinationModel();
    }
}
