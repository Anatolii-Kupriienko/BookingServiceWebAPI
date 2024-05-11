using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DbAccess.Models
{
    public class BaseModel
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

    }
}