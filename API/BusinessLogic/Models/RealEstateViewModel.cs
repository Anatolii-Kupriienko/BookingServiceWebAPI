using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RealEstateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public double SizeSquareMeters { get; set; }
        [Required]
        public double PricePerNight { get; set; }
        [Required]
        public RealEstateType Type { get; set; }
        [Required]
        public UserUpdateModel Owner { get; set; }
        [Required]
        public bool IsVacant { get; set; }
        public UserUpdateModel? OccupiedBy { get; set; }
        public string? Notes { get; set; }
    }
}