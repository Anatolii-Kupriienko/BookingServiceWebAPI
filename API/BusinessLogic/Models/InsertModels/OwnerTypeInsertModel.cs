namespace API.Models
{
    public class OwnerTypeInsertModel
    {
        public string Type { get; set; } = string.Empty;
    
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Type);
        }
    }
}