namespace truedrive_backend.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CarId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; set; }
    }
}
