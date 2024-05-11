using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DbAccess.Models
{
    [Table("OwnerType")]
    public class OwnerTypeModel : BaseModel
    {
        [Column("Type")]
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}