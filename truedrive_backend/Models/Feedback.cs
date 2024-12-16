namespace truedrive_backend.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string CustomerName { get; set; }
        public decimal Rating { get; set; }
        public string Text { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
