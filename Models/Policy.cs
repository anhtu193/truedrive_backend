namespace truedrive_backend.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
