namespace truedrive_backend.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
