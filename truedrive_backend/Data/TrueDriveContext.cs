using Microsoft.EntityFrameworkCore;

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
    }
}

