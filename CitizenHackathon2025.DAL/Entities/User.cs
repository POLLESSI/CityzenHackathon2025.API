namespace CitizenHackathon2025.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public string Role { get; set; } = "User";
        public bool Active { get; set; } = true;
    }
}

