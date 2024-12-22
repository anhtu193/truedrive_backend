namespace truedrive_backend.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Showroom { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string Note { get; set; }
    }
}
