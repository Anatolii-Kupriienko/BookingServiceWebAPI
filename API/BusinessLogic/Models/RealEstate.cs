using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RealEstate
    {
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
        public User Owner { get; set; }
        [Required]
        public bool IsVacant { get; set; }
        public User? OccupiedBy { get; set; }
        public string? Notes { get; set; }

        public bool IsValid() // this can be done differently and using a library like FluentValidation
        { // but since there are no real requirements for the data we receive and will not be used by actual clients, this will suffice
            return !string.IsNullOrEmpty(Location) && SizeSquareMeters > 0 && PricePerNight > 0 && Type != null && Owner != null;
        }
    }
}