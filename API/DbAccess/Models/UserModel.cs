using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace API.DbAccess.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        [ForeignKey("OwnerType")]
        public int OwnerTypeId { get; set; }

        public OwnerTypeModel OwnerType { get; set; }
    }
}