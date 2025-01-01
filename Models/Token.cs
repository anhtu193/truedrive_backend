namespace truedrive_backend.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string JwtToken { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
