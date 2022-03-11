namespace Cygnus.Domain.Models
{
    public enum FieldTypeModel
    {
        String,
        Number,
        Boolean
    }

    public class FieldModel
    {
        public string Name { get; set; } = string.Empty;

        public string MapFrom { get; set; } = string.Empty;

        public FieldTypeModel FieldType { get; set; }
    }
}
