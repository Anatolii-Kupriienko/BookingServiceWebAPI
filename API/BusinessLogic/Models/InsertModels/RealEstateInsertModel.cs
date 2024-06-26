using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RealEstateInsertModel
    {
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public double SizeSquareMeters { get; set; }
        [Required]
        public double PricePerNight { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public string OwnerId { get; set; } = string.Empty;
        [Required]
        public bool IsVacant { get; set; }
        public string? Notes { get; set; }


        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Location) && SizeSquareMeters > 0 && PricePerNight > 0 && TypeId != null && OwnerId != null;
        }
    }
}