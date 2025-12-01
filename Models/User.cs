namespace CartridgeWebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;  // ← ВЕРНИ Password вместо PasswordHash
        public string Role { get; set; } = string.Empty;
    }
}