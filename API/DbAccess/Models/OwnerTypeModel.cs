using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DbAccess.Models
{
    public class OwnerTypeModel : BaseModel
    {
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}