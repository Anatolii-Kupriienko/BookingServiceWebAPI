using System.ComponentModel.DataAnnotations;

namespace API.DbAccess.Models
{
    public class OwnerTypeModel : BaseModel
    {
        [Required]
        public string Type { get; set; } = string.Empty;
    }
}