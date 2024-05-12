using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RealEstateType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}