namespace API.Models
{
    public class RealEstateTypeInsertModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}