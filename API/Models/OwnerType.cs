using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("OwnerType")]
    public class OwnerType
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("Type")]
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}