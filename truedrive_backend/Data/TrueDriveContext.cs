using Microsoft.EntityFrameworkCore;
using truedrive_backend.Models;

namespace truedrive_backend.Data
{
    public class TrueDriveContext : DbContext
    {
        public TrueDriveContext(DbContextOptions<TrueDriveContext> options) : base(options)
        {
        }

        public DbSet<Models.Car> Car { get; set; }
        public DbSet<Models.Catalog> Catalog { get; set; }
        public DbSet<Models.Make> Make { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Feedback> Feedback { get; set; }
        public DbSet<Models.Blog> Blog { get; set; }
        public DbSet<Models.Policy> Policy { get; set; }
        public DbSet<Models.Token> Tokens { get; set; }
        public DbSet<Models.Showroom> Showroom { get; set; }
        public DbSet<Models.Appointment> Appointment { get; set; }
        public DbSet<Models.Wishlist> Wishlist { get; set; }
        public DbSet<Models.Transaction> Transaction { get; set; }
    }
}

