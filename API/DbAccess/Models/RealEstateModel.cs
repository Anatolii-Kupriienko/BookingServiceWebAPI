using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.DbAccess.Models
{
    [Table("RealEstate")]
    public class RealEstateModel : BaseModel
    {
        [Required]
        [Column("Location")]
        public string Location { get; set; } = string.Empty; // this can be a separate table but for simplicity we will keep it as a string

        [Required]
        [Column("SizeSquareMeters")]
        [Range(0, double.MaxValue)]
        public double SizeSquareMeters { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column("PricePerNight")]
        public double PricePerNight { get; set; }

        [ForeignKey("Type")]
        [Column("RealEstateTypeId")]
        public int RealEstateTypeId { get; set; }

        [ForeignKey("Owner")]
        [Column("OwnerId")]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [Column("IsVacant")]
        public bool IsVacant { get; set; }

        [ForeignKey("OccupiedBy")]
        [AllowNull]
        [Column("OccupiedById")]
        public string? OccupiedById { get; set; }

        [AllowNull]
        [Column("Notes")]
        public string? Notes { get; set; }

        public UserModel Owner { get; set; }

        public RealEstateTypeModel Type { get; set; }

        public UserModel? OccupiedBy { get; set; }
    }
}