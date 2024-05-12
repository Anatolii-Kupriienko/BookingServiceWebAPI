using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class OwnerType
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Type);
        }
    }
}