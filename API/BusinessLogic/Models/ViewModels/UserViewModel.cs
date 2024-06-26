namespace API.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public OwnerType? OwnerType { get; set; }
    }
}