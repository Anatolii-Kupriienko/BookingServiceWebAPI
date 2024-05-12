using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User // This class doesn't contain user's password since we don't want to send that information to the client
    {
        public string? Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? NewPassword { get; set; }
        [Required]
        public OwnerType OwnerType { get; set; }
    }
}