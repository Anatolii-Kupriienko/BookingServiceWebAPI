using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.DbAccess.Models
{
    [Table("RealEstateType")]
    public class RealEstateTypeModel : BaseModel
    {
        [Required]
        [MaxLength(70)]
        [MinLength(3)]
        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        [Column("Description")]
        public string? Description { get; set; } = string.Empty; //should be used mostly for "Other" type 
    }
}