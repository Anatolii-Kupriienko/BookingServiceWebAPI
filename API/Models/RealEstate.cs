namespace API.Models
{
    public class RealEstate
    {
        public string Location { get; set; } = string.Empty;
        public double SizeSquareMeters { get; set; }
        public double PricePerNight { get; set; }
        public RealEstateType Type { get; set; }
        public User Owner { get; set; }
        public bool IsVacant { get; set; }
        public User? OccupiedBy { get; set; }
        public string? Notes { get; set; }
    }
}