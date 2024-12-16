﻿using Microsoft.EntityFrameworkCore;
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
    }
}

