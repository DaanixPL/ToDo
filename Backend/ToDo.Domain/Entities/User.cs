namespace ToDo.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }


        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; } = false;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
