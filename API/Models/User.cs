namespace API.Models
{
    public class User // This class doesn't contain user's password since we don't want to send that information to the client
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public OwnerType OwnerType { get; set; }
    }
}