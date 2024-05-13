namespace API.Models
{
    public class OwnerTypeInsertModel
    {
        public string Name { get; set; } = string.Empty;
    
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
    }
}